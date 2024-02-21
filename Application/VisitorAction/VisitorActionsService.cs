using Application.Exceptions;
using AutoMapper;
using Domain.Entities.VisitorsAction;
using Repository.Skipass;
using Repository.VisitorActions;
using Domain;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;


namespace Application.VisitorAction;

internal sealed class VisitorActionsService : IVisitorActions
{
    private readonly IMapper mapper;
    private readonly ISkipassRepository skipassRepository;
    private readonly IVisitorActionsRepository visitorActionsRepository;
    private readonly HotelContext context;

    public VisitorActionsService(IMapper mapper, IVisitorActionsRepository visitorActionsRepository,
        ISkipassRepository skipassRepository, HotelContext context)
    {
        this.mapper = mapper;
        this.visitorActionsRepository = visitorActionsRepository;
        this.skipassRepository = skipassRepository;
        this.context = context;
    }

    public async Task<IReadOnlyCollection<GetVisitorActionsModel>> GetAllAsync(int offset, int limit)
    {
        return mapper.Map<IReadOnlyCollection<GetVisitorActionsModel>>(
            await visitorActionsRepository.GetListAsync(offset, limit));
    }

    public async Task<GetVisitorActionsModel> GetByIdAsync(Guid id)
    {
        var entity = await visitorActionsRepository.GetByIdAsync(id) ?? throw new NotFoundException();
        return mapper.Map<GetVisitorActionsModel>(entity);
    }

    public async Task<Guid> AddAsync(AddVisitorActionsModel model)
    {
        Guid result;
        using (var dbContextTransaction = await context.Database.BeginTransactionAsync())
        {
            var skipassRecord = (await skipassRepository.GetByIdAsync(model.SkipassId))!;

            if (skipassRecord == null) throw new NotFoundException("Skipass not found");

            if (!skipassRecord.Status)
                throw new SkipassStatusException("Your skipass is inactive. Please, contact administrators");

            var tariff = await context.Tariffs
                             .Include(s => s.Skipasses)
                             .Include(tr => tr.Tariffications)
                             .ThenInclude(e => e.Location)
                             .AsNoTracking()
                             .FirstOrDefaultAsync(e => e.Id == skipassRecord.TariffId)
                         ?? throw new NotFoundException("tariffication not found");
            
            model.BalanceChange ??= tariff.Tariffications
                .OrderByDescending(t => t.Price)
                .First(location => location.LocationId == model.LocationId).Price;
            
            
            
            if (model.TransactionType == OperationType.Negative)
            {
                if (tariff.IsVip)
                {
                    model.BalanceChange = 0;
                }
                
                if (skipassRecord.Balance - model.BalanceChange < 0)
                {
                    throw new SkipassStatusException("Not enough balance");
                }
                
                skipassRecord.Balance -= (int)model.BalanceChange * tariff.PriceModifier;
            }
            
            else
            {
                skipassRecord.Balance += (int)model.BalanceChange;
            }
        
            await skipassRepository.UpdateAsync(skipassRecord);
            result = await visitorActionsRepository.AddAsync(mapper.Map<VisitorActionsRecord>(model));
            await dbContextTransaction.CommitAsync();
        }
        
        return result;
    }
    
    public async Task<Guid> TapSkipass(AddVisitorActionsModel model)
    {
        model.TransactionType = OperationType.Negative;
       
        return await AddAsync(model);
    }

    public async Task<bool> UpdateAsync(UpdateVisitorActionsModel model)
    {
        if (await visitorActionsRepository.GetByIdAsync(model.Id) == null) throw new NotFoundException();

        var skipassRecord = (await skipassRepository.GetByIdAsync(model.SkipassId))!;

        if (!skipassRecord.Status)
            throw new SkipassStatusException("Your skipass is inactive. Please, contact administrators");

        var tariff = await context.Tariffs
                         .Include(s => s.Skipasses)
                         .Include(tr => tr.Tariffications)
                         .ThenInclude(e => e.Location)
                         .AsNoTracking()
                         .FirstOrDefaultAsync(e => e.Id == skipassRecord.TariffId)
                     ?? throw new NotFoundException("tariffication not found");

        model.BalanceChange ??= tariff.Tariffications
            .OrderByDescending(t => t.Price)
            .First(location => location.LocationId == model.LocationId).Price;
            

        if (model.TransactionType == OperationType.Negative && !tariff.IsVip)
        {
            skipassRecord.Balance -= (int)model.BalanceChange;
        }
            
        else
        {
            skipassRecord.Balance += (int)model.BalanceChange;
        }
        
        await skipassRepository.UpdateAsync(skipassRecord);

        
        return await visitorActionsRepository.UpdateAsync(mapper.Map<VisitorActionsRecord>(model));
    }

    public async Task DeleteAsync(Guid id)
    {
        var visitorsAction = mapper.Map<VisitorActionsRecord>(await GetByIdAsync(id));
        var skipassRecord = (await skipassRepository.GetByIdAsync(visitorsAction.SkipassId))
                            ?? throw new NotFoundException("Skipass not found");

        if (visitorsAction.TransactionType == OperationType.Positive)
        {
            skipassRecord.Balance -= visitorsAction.BalanceChange;
        }
        else
        {
            skipassRecord.Balance += visitorsAction.BalanceChange;
        }
        
        await skipassRepository.UpdateAsync(skipassRecord);
        await visitorActionsRepository.DeleteAsync(id);
    }
}
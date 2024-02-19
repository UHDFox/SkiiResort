using Application.Exceptions;
using AutoMapper;
using Domain.Entities.VisitorsAction;
using Repository.Skipass;
using Repository.VisitorActions;
using System.Transactions;
using Domain;
using Repository.Location;
using Repository.Tariff;

namespace Application.VisitorAction;

internal sealed class VisitorActionsService : IVisitorActions
{
    private readonly IMapper mapper;
    private readonly ISkipassRepository skipassRepository;
    private readonly IVisitorActionsRepository visitorActionsRepository;
    private readonly HotelContext context;
    public readonly ILocationRepository locationRepository;

    public VisitorActionsService(IMapper mapper, IVisitorActionsRepository visitorActionsRepository,
        ISkipassRepository skipassRepository, HotelContext context, ILocationRepository locationRepository)
    {
        this.mapper = mapper;
        this.visitorActionsRepository = visitorActionsRepository;
        this.skipassRepository = skipassRepository;
        this.context = context;
        this.locationRepository = locationRepository;
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
       /* var skipassRecord = (await skipassRepository.GetByIdAsync(model.SkipassId))!;
        if (await skipassRepository.GetByIdAsync(skipassRecord.Id) == null) throw new NotFoundException();
        if (!skipassRecord.Status)
            throw new SkipassStatusException("Your skipass is inactive. Please, contact administrators");

        /*if (!skipassRecord.IsVip || (skipassRecord.IsVip && model.BalanceChange >= 0))
        {
            skipassRecord.Balance += model.BalanceChange;
            await skipassRepository.UpdateAsync(skipassRecord);
        }

        return await visitorActionsRepository.AddAsync(mapper.Map<VisitorActionsRecord>(model));*/
       
       var skipassRecord = (await skipassRepository.GetByIdAsync(model.SkipassId))!;

       var tariff = skipassRecord.Tariff ?? throw new NotFoundException();

       var tariffications = tariff.Tariffications;
       var tariffication = tariff.Tariffications.First(e => e.LocationId == model.LocationId) 
                           ?? throw new NotFoundException("location not found");

       var price = tariffication.Price;
       /*var tarifficationEntity = skipassRecord.Tariff!.Tariffications.FirstOrDefault(e => e.LocationId == locationId)
                    ?? throw new NotFoundException();
                    
        var price = tarifficationEntity.Price;*/


       if (skipassRecord.Balance - price < 0)
       {
           throw new Exception("not enough balance");
       }

       Guid resultId;
       using (var transaction = context.Database.BeginTransaction())
       {
           model.BalanceChange = price;
           resultId = await visitorActionsRepository.AddAsync(mapper.Map<VisitorActionsRecord>(model));
           await transaction.CommitAsync();
       }

       return resultId;
    }

    public async Task<bool> UpdateAsync(UpdateVisitorActionsModel model)
    {
        if (await visitorActionsRepository.GetByIdAsync(model.Id) == null) throw new NotFoundException();

        var skipassRecord = (await skipassRepository.GetByIdAsync(model.SkipassId))!;

        if (!skipassRecord.Status)
            throw new SkipassStatusException("Your skipass is inactive. Please, contact administrators");

        /*if (!skipassRecord.IsVip || (skipassRecord.IsVip && model.BalanceChange >= 0))
        {
            skipassRecord.Balance += model.BalanceChange;
            await skipassRepository.UpdateAsync(skipassRecord);
        }*/

        return await visitorActionsRepository.UpdateAsync(mapper.Map<VisitorActionsRecord>(model));
    }

    public async Task DeleteAsync(Guid id)
    {
        await GetByIdAsync(id);
        await visitorActionsRepository.DeleteAsync(id);
    }
}
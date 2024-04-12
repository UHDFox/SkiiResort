using Application.Exceptions;
using AutoMapper;
using Domain.Entities.VisitorsAction;
using Domain.Enums;
using Repository.Location;
using Repository.Skipass;
using Repository.Tariff;
using Repository.Tariffication;
using Repository.VisitorActions;

namespace Application.VisitorAction;

internal sealed class VisitorActionsService : IVisitorActions
{
    private readonly ILocationRepository locationRepository;
    private readonly IMapper mapper;
    private readonly ISkipassRepository skipassRepository;
    private readonly ITarifficationRepository tarifficationRepository;
    private readonly ITariffRepository tariffRepository;
    private readonly IVisitorActionsRepository visitorActionsRepository;

    public VisitorActionsService(IMapper mapper,
        IVisitorActionsRepository visitorActionsRepository,
        ISkipassRepository skipassRepository,
        ITariffRepository tariffRepository,
        ILocationRepository locationRepository,
        ITarifficationRepository tarifficationRepository)
    {
        this.mapper = mapper;
        this.visitorActionsRepository = visitorActionsRepository;
        this.skipassRepository = skipassRepository;
        this.tariffRepository = tariffRepository;
        this.locationRepository = locationRepository;
        this.tarifficationRepository = tarifficationRepository;
    }

    public async Task<IReadOnlyCollection<GetVisitorActionsModel>> GetAllAsync(int offset, int limit)
    {
        var totalAmount = await visitorActionsRepository.GetTotalAmountAsync();

        if (totalAmount < offset)
        {
            throw new PaginationQueryException("offset exceeds total amount of records");
        }

        if (totalAmount < offset + limit)
        {
            throw new PaginationQueryException("queried page exceeds total amount of records");
        }

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

        await using (var dbContextTransaction = await visitorActionsRepository.BeginTransaction())
        {
            var skipassRecord = await skipassRepository.GetByIdAsync(model.SkipassId)
                                ?? throw new NotFoundException("Skipass not found");

            if (!skipassRecord.Status)
            {
                throw new SkipassStatusException("Your skipass is inactive. Please, contact administrators");
            }

            var tariff = await tariffRepository.GetByIdAsync(skipassRecord.TariffId)
                         ?? throw new NotFoundException("couldn't find tariff related to the current skipass");

            var location = await locationRepository.GetByIdAsync(model.LocationId)
                           ?? throw new NotFoundException("current tariff doesn't set the price for that location");

            model.BalanceChange ??=
                (await tarifficationRepository.GetByLocationAndTariffIdsAsync(skipassRecord.TariffId, location.Id)
                 ?? throw new NotFoundException("couldn't find tariffication related to such tariff and location")).Price;


            switch (model.TransactionType)
            {
                case OperationType.Positive:
                    skipassRecord.Balance += (double)model.BalanceChange;
                    break;

                case OperationType.Negative:
                    if (tariff.IsVip)
                    {
                        model.BalanceChange = 0;
                    }

                    if (skipassRecord.Balance - model.BalanceChange < 0)
                    {
                        throw new SkipassStatusException("Not enough balance");
                    }

                    skipassRecord.Balance -= (double)model.BalanceChange;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(model.TransactionType));
            }

            skipassRepository.UpdateAsync(skipassRecord);

            result = await visitorActionsRepository.AddAsync(mapper.Map<VisitorActionsRecord>(model));
            await visitorActionsRepository.SaveChangesAsync();
            await dbContextTransaction.CommitAsync();
        }

        return result;
    }

    public async Task<Guid> TapSkipass(AddVisitorActionsModel model)
    {
        model.TransactionType = OperationType.Negative;

        return await AddAsync(model);
    }

    public async Task<Guid> DepositSkipassBalance(AddVisitorActionsModel model)
    {
        model.TransactionType = OperationType.Positive;
        model.Time = DateTimeOffset.UtcNow;
        return await AddAsync(model);
    }

    public async Task<UpdateVisitorActionsModel> UpdateAsync(UpdateVisitorActionsModel model)
    {
        await using (var dbContextTransaction = await visitorActionsRepository.BeginTransaction())
        {
            var entity = await visitorActionsRepository.GetByIdAsync(model.Id)
                         ?? throw new NotFoundException("Visitors action not found");

            var skipassRecord = await skipassRepository.GetByIdAsync(model.SkipassId)
                                ?? throw new NotFoundException("skipass not found");

            if (!skipassRecord.Status)
            {
                throw new SkipassStatusException("Your skipass is inactive. Please, contact administrators");
            }

            var location = await locationRepository.GetByIdAsync(model.LocationId)
                           ?? throw new NotFoundException("current tariff doesn't set the price for that location");

            model.BalanceChange ??=
                (await tarifficationRepository.GetByLocationAndTariffIdsAsync(skipassRecord.TariffId, location.Id)
                 ?? throw new NotFoundException("couldn't find tariffication related to such tariff and location")).Price;


            if (model.BalanceChange != entity.BalanceChange || model.TransactionType != entity.TransactionType)
            {
                switch (model.TransactionType)
                {
                    case OperationType.Positive:
                        skipassRecord.Balance += (double)model.BalanceChange;
                        break;

                    case OperationType.Negative:
                        skipassRecord.Balance -= (double)model.BalanceChange;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(model.TransactionType));
                }
            }


            skipassRepository.UpdateAsync(skipassRecord);


            mapper.Map(model, entity);
            visitorActionsRepository.UpdateAsync(entity);
            await visitorActionsRepository.SaveChangesAsync();

            await dbContextTransaction.CommitAsync();
        }

        return model;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var visitorsAction = mapper.Map<VisitorActionsRecord>(await GetByIdAsync(id));
        var skipassRecord = await skipassRepository.GetByIdAsync(visitorsAction.SkipassId)
                            ?? throw new NotFoundException("Skipass not found");
        bool result;
        await using (var dbContextTransaction =
                     await visitorActionsRepository.BeginTransaction())
        {
            switch (visitorsAction.TransactionType)
            {
                case OperationType.Positive:
                    skipassRecord.Balance -= visitorsAction.BalanceChange;
                    break;

                case OperationType.Negative:
                    skipassRecord.Balance += visitorsAction.BalanceChange;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(visitorsAction.TransactionType));
            }

            skipassRepository.UpdateAsync(skipassRecord);
            result = await visitorActionsRepository.DeleteAsync(id);
            await dbContextTransaction.CommitAsync();
        }

        return result;
    }
}

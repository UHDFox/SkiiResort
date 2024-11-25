using Application;
using AutoMapper;
using SkiiResort.Application.Exceptions;
using SkiiResort.Domain.Entities.VisitorsAction;
using SkiiResort.Domain.Enums;
using SkiiResort.Repository.Location;
using SkiiResort.Repository.Skipass;
using SkiiResort.Repository.Tariff;
using SkiiResort.Repository.Tariffication;
using SkiiResort.Repository.VisitorActions;

namespace SkiiResort.Application.VisitorAction;

internal sealed class VisitorActionsService : Service<VisitorActionsModel, VisitorActionsRecord>, IVisitorActionsService
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
        : base(visitorActionsRepository, mapper)
    {
        this.mapper = mapper;
        this.visitorActionsRepository = visitorActionsRepository;
        this.skipassRepository = skipassRepository;
        this.tariffRepository = tariffRepository;
        this.locationRepository = locationRepository;
        this.tarifficationRepository = tarifficationRepository;
    }

    public new async Task<Guid> AddAsync(VisitorActionsModel model)
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

        return await AddAsync(mapper.Map<VisitorActionsModel>(model));
    }

    public async Task<Guid> DepositSkipassBalance(AddVisitorActionsModel model)
    {
        model.TransactionType = OperationType.Positive;
        model.Time = DateTimeOffset.UtcNow;
        return await AddAsync(mapper.Map<VisitorActionsModel>(model));
    }

    public new async Task<VisitorActionsModel> UpdateAsync(VisitorActionsModel model)
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

    public new async Task<bool> DeleteAsync(Guid id)
    {
        var visitorActionRecord = mapper.Map<VisitorActionsRecord>(await GetByIdAsync(id));
        var skipassRecord = await skipassRepository.GetByIdAsync(visitorActionRecord.SkipassId)
                            ?? throw new NotFoundException("Skipass not found");
        bool result;
        await using (var dbContextTransaction =
                     await visitorActionsRepository.BeginTransaction())
        {
            switch (visitorActionRecord.TransactionType)
            {
                case OperationType.Positive:
                    skipassRecord.Balance -= visitorActionRecord.BalanceChange;
                    break;

                case OperationType.Negative:
                    skipassRecord.Balance += visitorActionRecord.BalanceChange;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(visitorActionRecord.TransactionType));
            }

            skipassRepository.UpdateAsync(skipassRecord);
            result = await visitorActionsRepository.DeleteAsync(id);
            await dbContextTransaction.CommitAsync();
        }

        return result;
    }
}

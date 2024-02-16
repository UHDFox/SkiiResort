using Application.Exceptions;
using AutoMapper;
using Domain.Entities.VisitorsAction;
using Repository.Skipass;
using Repository.VisitorActions;

namespace Application.VisitorAction;

internal sealed class VisitorActionsService : IVisitorActions
{
    private readonly IMapper mapper;
    private readonly ISkipassRepository skipassRepository;
    private readonly IVisitorActionsRepository visitorActionsRepository;

    public VisitorActionsService(IMapper mapper, IVisitorActionsRepository visitorActionsRepository,
        ISkipassRepository skipassRepository)
    {
        this.mapper = mapper;
        this.visitorActionsRepository = visitorActionsRepository;
        this.skipassRepository = skipassRepository;
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
        var skipassRecord = (await skipassRepository.GetByIdAsync(model.SkipassId))!;
        if (await skipassRepository.GetByIdAsync(skipassRecord.Id) == null) throw new NotFoundException();
        if (!skipassRecord.Status)
            throw new SkipassStatusException("Your skipass is inactive. Please, contact administrators");

        /*if (!skipassRecord.IsVip || (skipassRecord.IsVip && model.BalanceChange >= 0))
        {
            skipassRecord.Balance += model.BalanceChange;
            await skipassRepository.UpdateAsync(skipassRecord);
        }*/

        return await visitorActionsRepository.AddAsync(mapper.Map<VisitorActionsRecord>(model));
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
using Application.Exceptions;
using AutoMapper;
using Domain;
using Domain.Entities.Tariff;
using Domain.Entities.VisitorsAction;
using Microsoft.EntityFrameworkCore;

namespace Application.VisitorAction;

internal sealed class VisitorActionsService : IVisitorActions
{
    private readonly HotelContext context;
    private readonly IMapper mapper;

    public VisitorActionsService(HotelContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }
    
    public async Task<IReadOnlyCollection<GetVisitorActionsModel>> GetAllAsync(int offset, int limit)
    {
        return mapper.Map<IReadOnlyCollection<GetVisitorActionsModel>>(await context.VisitorActions.Skip(offset).Take(limit).ToListAsync());
    }

    public async Task<VisitorActionsRecord> GetByIdAsync(Guid id)
    {
        return await context.VisitorActions.FindAsync(id) ?? throw new NotFoundException();
    }

    public async Task<VisitorActionsRecord> AddAsync(AddVisitorActionsModel model)
    {
        var result = await context.VisitorActions.AddAsync(mapper.Map<VisitorActionsRecord>(model));
        await context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<bool> UpdateAsync(UpdateVisitorActionsModel model)
    {
        var record = GetByIdAsync(model.Id);
        mapper.Map<VisitorActionsRecord>(model);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task DeleteAsync(Guid id)
    {
        var record = await GetByIdAsync(id);
        context.VisitorActions.Remove(record);
        await context.SaveChangesAsync();
    }
}
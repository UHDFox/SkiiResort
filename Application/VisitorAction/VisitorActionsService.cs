using Application.Exceptions;
using Application.Skipass;
using AutoMapper;
using Domain;
using Domain.Entities.Skipass;
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

    public async Task<GetVisitorActionsModel> GetByIdAsync(Guid id)
    {
        var entity = await context.VisitorActions.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new NotFoundException();
        return mapper.Map<GetVisitorActionsModel>(entity);
    }

    public async Task<VisitorActionsRecord> AddAsync(AddVisitorActionsModel model)
    {
        var result = await context.VisitorActions.AddAsync(mapper.Map<VisitorActionsRecord>(model));
        await context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<bool> UpdateAsync(UpdateVisitorActionsModel model)
    {
        await GetByIdAsync(model.Id);
        context.VisitorActions.Update(        mapper.Map<VisitorActionsRecord>(model));
        return await context.SaveChangesAsync() > 0;
    }

    public async Task DeleteAsync(Guid id)
    {
        var record = await GetByIdAsync(id);
        context.VisitorActions.Remove(mapper.Map<VisitorActionsRecord>(record));
        await context.SaveChangesAsync();
    }
}
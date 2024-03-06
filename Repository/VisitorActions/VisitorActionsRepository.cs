using Domain;
using Domain.Entities.VisitorsAction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Repository.VisitorActions;

internal sealed class VisitorActionsRepository : IVisitorActionsRepository
{
    private readonly HotelContext context;

    public VisitorActionsRepository(HotelContext context)
    {
        this.context = context;
    }

    public async Task<IReadOnlyCollection<VisitorActionsRecord>> GetListAsync(int offset, int limit)
    {
        return await context.VisitorActions.Skip(offset).Take(limit).ToListAsync();
    }

    public async Task<VisitorActionsRecord?> GetByIdAsync(Guid id)
    {
        return await context.VisitorActions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Guid> AddAsync(VisitorActionsRecord data)
    {
        await context.VisitorActions.AddAsync(data);
        await SaveChangesAsync();
        return data.Id;
    }

    public async Task<bool> UpdateAsync(VisitorActionsRecord data)
    {
        context.VisitorActions.Update(data);
        return await SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        context.VisitorActions.Remove((await GetByIdAsync(id))!);
        return await SaveChangesAsync() > 0;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }

    public async Task<IDbContextTransaction> BeginTransaction()
    {
        return await context.Database.BeginTransactionAsync();
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SkiiResort.Domain;
using SkiiResort.Domain.Entities.VisitorsAction;

namespace SkiiResort.Repository.VisitorActions;

internal sealed class VisitorActionsRepository : IVisitorActionsRepository
{
    private readonly SkiiResortContext context;

    public VisitorActionsRepository(SkiiResortContext context)
    {
        this.context = context;
    }

    public async Task<IReadOnlyCollection<VisitorActionsRecord>> GetAllAsync(int offset, int limit)
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

    public void UpdateAsync(VisitorActionsRecord data)
    {
        context.VisitorActions.Update(data);
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

    public async Task<int> GetTotalAmountAsync()
    {
        return await context.VisitorActions.CountAsync();
    }
}

using Microsoft.EntityFrameworkCore;
using SkiiResort.Domain;
using SkiiResort.Domain.Entities.Visitor;

namespace SkiiResort.Repository.Visitor;

internal sealed class VisitorRepository : IVisitorRepository
{
    private readonly SkiiResortContext context;

    public VisitorRepository(SkiiResortContext context)
    {
        this.context = context;
    }

    public async Task<IReadOnlyCollection<VisitorRecord>> GetListAsync(int offset, int limit)
    {
        return await context.Visitors.Skip(offset).Take(limit).ToListAsync();
    }

    public async Task<int> GetTotalAmountAsync()
    {
        return await context.Visitors.CountAsync();
    }

    public async Task<VisitorRecord?> GetByIdAsync(Guid id)
    {
        return await context.Visitors.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Guid> AddAsync(VisitorRecord data)
    {
        await context.Visitors.AddAsync(data);
        await SaveChangesAsync();
        return data.Id;
    }

    public void UpdateAsync(VisitorRecord data)
    {
        context.Visitors.Update(data);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        context.Visitors.Remove((await GetByIdAsync(id))!);
        return await SaveChangesAsync() > 0;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }
}

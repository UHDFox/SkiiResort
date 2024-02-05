using Domain;
using Domain.Entities.Visitor;
using Microsoft.EntityFrameworkCore;

namespace Repository.Visitor;

internal sealed class VisitorRepository : IVisitorRepository
{
    private readonly HotelContext context;

    public VisitorRepository(HotelContext context) => this.context = context;
    
    public async Task<IReadOnlyCollection<VisitorRecord>> GetListAsync(int offset, int limit)
    {
        return await context.Visitors.Skip(offset).Take(limit).ToListAsync();
    }

    public async Task<VisitorRecord?> GetByIdAsync(Guid id)
    {
        return await context.Visitors.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Guid> AddAsync(VisitorRecord data)
    {
        await context.Visitors.AddAsync(data);
        await context.SaveChangesAsync();
        return data.Id;
    }

    public async Task<bool> UpdateAsync(VisitorRecord data)
    {
        context.Visitors.Update(data);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task DeleteAsync(Guid id)
    {
        context.Visitors.Remove((await GetByIdAsync(id))!);
        var result = await context.SaveChangesAsync();
    }
}
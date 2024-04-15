using Microsoft.EntityFrameworkCore;
using SkiiResort.Domain;
using SkiiResort.Domain.Entities.Skipass;

namespace SkiiResort.Repository.Skipass;

internal sealed class SkipassRepository : ISkipassRepository
{
    private readonly SkiiResortContext context;


    public SkipassRepository(SkiiResortContext context)
    {
        this.context = context;
    }

    public async Task<IReadOnlyCollection<SkipassRecord>> GetListAsync(int offset, int limit)
    {
        return await context.Skipasses.Skip(offset).Take(limit).ToListAsync();
    }

    public async Task<int> GetTotalAmountAsync()
    {
        return await context.Visitors.CountAsync();
    }

    public async Task<SkipassRecord?> GetByIdAsync(Guid skipassId)
    {
        return await context.Skipasses
            .AsNoTracking()
            .Include(record => record.Tariff)
            .Include(record => record.Visitor)
            .FirstOrDefaultAsync(x => x.Id == skipassId);
    }

    public async Task<Guid> AddAsync(SkipassRecord data)
    {
        var result = await context.Skipasses.AddAsync(data);
        await SaveChangesAsync();
        return result.Entity.Id;
    }

    public void UpdateAsync(SkipassRecord data)
    {
        context.Skipasses.Update(data);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        context.Skipasses.Remove((await GetByIdAsync(id))!);
        return await SaveChangesAsync() > 0;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }
}

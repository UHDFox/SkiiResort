using Domain;
using Domain.Entities.Skipass;
using Microsoft.EntityFrameworkCore;

namespace Repository.Skipass;

internal sealed class SkipassRepository : ISkipassRepository
{
    private readonly HotelContext context;

    public SkipassRepository(HotelContext context)
    {
        this.context = context;
    }

    public async Task<IReadOnlyCollection<SkipassRecord>> GetListAsync(int offset, int limit)
    {
        return await context.Skipasses.Skip(offset).Take(limit).ToListAsync();
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
        await context.SaveChangesAsync();
        return result.Entity.Id;
    }

    public async Task<bool> UpdateAsync(SkipassRecord data)
    {
        context.Skipasses.Update(data);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        context.Skipasses.Remove((await GetByIdAsync(id))!);
        return await context.SaveChangesAsync() > 0;
    }
}
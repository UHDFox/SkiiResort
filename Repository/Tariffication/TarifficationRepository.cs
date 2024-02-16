using Domain;
using Domain.Entities.Tariffication;
using Microsoft.EntityFrameworkCore;

namespace Repository.Tariffication;

internal sealed class TarifficationRepository : ITarifficationRepository
{
    private readonly HotelContext context;

    public TarifficationRepository(HotelContext context)
    {
        this.context = context;
    }
    
    public async Task<IReadOnlyCollection<TarifficationRecord>> GetListAsync(int offset, int limit)
    {
        return await context.Tariffications.Skip(offset).Take(limit).ToListAsync();
    }

    public async Task<TarifficationRecord?> GetByIdAsync(Guid id)
    {
        return await context.Tariffications
            .AsNoTracking()
            .Include(x => x.Location)
            .Include(x => x.Tariff)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Guid> AddAsync(TarifficationRecord data)
    {
        var result = await context.Tariffications.AddAsync(data);
        await context.SaveChangesAsync();
        return result.Entity.Id;
    }

    public async Task<bool> UpdateAsync(TarifficationRecord data)
    {
        context.Tariffications.Update(data);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task DeleteAsync(Guid id)
    {
        context.Tariffications.Remove((await GetByIdAsync(id))!);
        await context.SaveChangesAsync();
    }
}
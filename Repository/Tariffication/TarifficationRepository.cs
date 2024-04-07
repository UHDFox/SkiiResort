using Domain;
using Domain.Entities.Tariffication;
using Microsoft.EntityFrameworkCore;

namespace Repository.Tariffication;

internal sealed class TarifficationRepository : ITarifficationRepository
{
    private readonly SkiiResortContext context;
    
    public TarifficationRepository(SkiiResortContext context)
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
        await SaveChangesAsync();
        return result.Entity.Id;
    }

    public void UpdateAsync(TarifficationRecord data)
    {
        context.Tariffications.Update(data);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        context.Tariffications.Remove((await GetByIdAsync(id))!);
        return await SaveChangesAsync() > 0;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }

    public async Task<TarifficationRecord> GetByLocationAndTariffIdsAsync(Guid tariffId, Guid locationId)
    {
        return await context.Tariffications
            .AsNoTracking()
            .Where(t => t.TariffId == tariffId)
            .OrderByDescending(t => t.Price)
            .FirstAsync(t => t.LocationId == locationId);
    }
}
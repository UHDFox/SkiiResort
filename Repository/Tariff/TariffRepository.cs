using Domain;
using Domain.Entities.Tariff;
using Microsoft.EntityFrameworkCore;

namespace Repository.Tariff;

internal sealed class TariffRepository : ITariffRepository
{
    private readonly HotelContext context;

    public TariffRepository(HotelContext context)
    {
        this.context = context;
    }

    public async Task<IReadOnlyCollection<TariffRecord>> GetListAsync(int offset, int limit)
    {
        return await context.Tariffs.Skip(offset).Take(limit).ToListAsync();
    }

    public async Task<TariffRecord?> GetByIdAsync(Guid id)
    {
        return await context.Tariffs.AsNoTracking().FirstOrDefaultAsync(record => record.Id == id);
    }

    public async Task<Guid> AddAsync(TariffRecord data)
    {
        await context.Tariffs.AddAsync(data);
        await context.SaveChangesAsync();
        return data.Id;
    }

    public async Task<bool> UpdateAsync(TariffRecord data)
    {
        context.Tariffs.Update(data);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        context.Tariffs.Remove((await GetByIdAsync(id))!);
        return await context.SaveChangesAsync() > 0;
    }
}
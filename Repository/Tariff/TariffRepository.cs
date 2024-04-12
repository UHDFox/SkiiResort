using Domain;
using Domain.Entities.Tariff;
using Microsoft.EntityFrameworkCore;


namespace Repository.Tariff;

internal sealed class TariffRepository : ITariffRepository
{
    private readonly SkiiResortContext context;


    public TariffRepository(SkiiResortContext context)
    {
        this.context = context;
    }

    public async Task<IReadOnlyCollection<TariffRecord>> GetListAsync(int offset, int limit)
    {
        return await context.Tariffs.Skip(offset).Take(limit).ToListAsync();
    }

    public async Task<int> GetTotalAmountAsync()
    {
        return await context.Visitors.CountAsync();
    }

    public async Task<TariffRecord?> GetByIdAsync(Guid id)
    {
        return await context.Tariffs.AsNoTracking().FirstOrDefaultAsync(record => record.Id == id);
    }

    public async Task<Guid> AddAsync(TariffRecord data)
    {
        await context.Tariffs.AddAsync(data);
        await SaveChangesAsync();
        return data.Id;
    }

    public void UpdateAsync(TariffRecord data)
    {
        context.Tariffs.Update(data);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        context.Tariffs.Remove((await GetByIdAsync(id))!);
        return await SaveChangesAsync() > 0;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }
}

using Microsoft.EntityFrameworkCore;
using SkiiResort.Domain;
using SkiiResort.Domain.Entities.Tariff;

namespace SkiiResort.Repository.Tariff;

internal sealed class TariffRepository : ITariffRepository
{
    private readonly SkiiResortContext context;


    public TariffRepository(SkiiResortContext context)
    {
        this.context = context;
    }

    public async Task<IReadOnlyCollection<TariffRecord>> GetAllAsync(int offset, int limit) =>
        await context.Tariffs.Skip(offset).Take(limit).ToListAsync();

    public async Task<int> GetTotalAmountAsync() => await context.Tariffs.CountAsync();

    public async Task<TariffRecord?> GetByIdAsync(Guid id) => await context.Tariffs.AsNoTracking().FirstOrDefaultAsync(record => record.Id == id);

    public async Task<Guid> AddAsync(TariffRecord data)
    {
        await context.Tariffs.AddAsync(data);
        await SaveChangesAsync();
        return data.Id;
    }

    public void Update(TariffRecord data) => context.Tariffs.Update(data);

    public async Task<bool> DeleteAsync(Guid id)
    {
        context.Tariffs.Remove((await GetByIdAsync(id))!);
        return await SaveChangesAsync() > 0;
    }

    public async Task<int> SaveChangesAsync() => await context.SaveChangesAsync();
}

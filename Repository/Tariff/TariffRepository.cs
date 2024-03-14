using Domain;
using Domain.Entities.Tariff;
using Microsoft.EntityFrameworkCore;
using Repository.DbContextRepository;

namespace Repository.Tariff;

internal sealed class TariffRepository : ITariffRepository
{
    private readonly IDbContextRepository<SkiiResortContext> dbContextRepository;


    public TariffRepository(IDbContextRepository<SkiiResortContext> dbContextRepository)
    {
        this.dbContextRepository = dbContextRepository;
    }

    public async Task<IReadOnlyCollection<TariffRecord>> GetListAsync(int offset, int limit)
    {
        return await dbContextRepository.GetDbContext().Tariffs.Skip(offset).Take(limit).ToListAsync();
    }

    public async Task<TariffRecord?> GetByIdAsync(Guid id)
    {
        return await dbContextRepository.GetDbContext().Tariffs.AsNoTracking().FirstOrDefaultAsync(record => record.Id == id);
    }

    public async Task<Guid> AddAsync(TariffRecord data)
    {
        await dbContextRepository.GetDbContext().Tariffs.AddAsync(data);
        await SaveChangesAsync();
        return data.Id;
    }

    public async Task<bool> UpdateAsync(TariffRecord data)
    {
        dbContextRepository.GetDbContext().Tariffs.Update(data);
        return await SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        dbContextRepository.GetDbContext().Tariffs.Remove((await GetByIdAsync(id))!);
        return await SaveChangesAsync() > 0;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await dbContextRepository.GetDbContext().SaveChangesAsync();
    }
}
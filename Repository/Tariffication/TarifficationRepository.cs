using Domain;
using Domain.Entities.Tariffication;
using Microsoft.EntityFrameworkCore;
using Repository.DbContextRepository;

namespace Repository.Tariffication;

internal sealed class TarifficationRepository : ITarifficationRepository
{
    private readonly IDbContextRepository<SkiiResortContext> dbContextRepository;


    public TarifficationRepository(IDbContextRepository<SkiiResortContext> dbContextRepository)
    {
        this.dbContextRepository = dbContextRepository;
    }
    
    public async Task<IReadOnlyCollection<TarifficationRecord>> GetListAsync(int offset, int limit)
    {
        return await dbContextRepository.GetDbContext().Tariffications.Skip(offset).Take(limit).ToListAsync();
    }

    public async Task<TarifficationRecord?> GetByIdAsync(Guid id)
    {
        return await dbContextRepository.GetDbContext().Tariffications
            .AsNoTracking()
            .Include(x => x.Location)
            .Include(x => x.Tariff)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Guid> AddAsync(TarifficationRecord data)
    {
        var result = await dbContextRepository.GetDbContext().Tariffications.AddAsync(data);
        await SaveChangesAsync();
        return result.Entity.Id;
    }

    public async Task<bool> UpdateAsync(TarifficationRecord data)
    {
        dbContextRepository.GetDbContext().Tariffications.Update(data);
        return await SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        dbContextRepository.GetDbContext().Tariffications.Remove((await GetByIdAsync(id))!);
        return await SaveChangesAsync() > 0;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await dbContextRepository.GetDbContext().SaveChangesAsync();
    }
}
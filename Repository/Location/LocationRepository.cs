using Domain;
using Domain.Entities.Location;
using Microsoft.EntityFrameworkCore;
using Repository.DbContextRepository;

namespace Repository.Location;

internal sealed class LocationRepository : ILocationRepository
{
    private readonly IDbContextRepository<SkiiResortContext> dbContextRepository;

    public LocationRepository(IDbContextRepository<SkiiResortContext> dbContextRepository)
    {
        this.dbContextRepository = dbContextRepository;
    }
    public async Task<IReadOnlyCollection<LocationRecord>> GetListAsync(int offset, int limit)
    {
        return await dbContextRepository.GetDbContext().Locations.Skip(offset).Take(limit).ToListAsync();
    }

    public async Task<LocationRecord?> GetByIdAsync(Guid id)
    {
        return await dbContextRepository.GetDbContext().Locations
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Guid> AddAsync(LocationRecord data)
    {
        var result = await dbContextRepository.GetDbContext().Locations.AddAsync(data);
        await SaveChangesAsync();
        return result.Entity.Id;
    }

    public async Task<bool> UpdateAsync(LocationRecord data)
    {
        dbContextRepository.GetDbContext().Locations.Update(data);
        return await SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var record = await GetByIdAsync(id);
        dbContextRepository.GetDbContext().Locations.Remove(record!);
        return await SaveChangesAsync() > 0;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await dbContextRepository.GetDbContext().SaveChangesAsync();
    }
}
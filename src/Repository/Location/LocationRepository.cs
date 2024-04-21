using Microsoft.EntityFrameworkCore;
using SkiiResort.Domain;
using SkiiResort.Domain.Entities.Location;

namespace SkiiResort.Repository.Location;

internal sealed class LocationRepository : ILocationRepository
{
    private readonly SkiiResortContext context;

    public LocationRepository(SkiiResortContext context)
    {
        this.context = context;
    }

    public async Task<IReadOnlyCollection<LocationRecord>> GetAllAsync(int offset, int limit) =>
        await context.Locations.Skip(offset).Take(limit).ToListAsync();

    public async Task<int> GetTotalAmountAsync() => await context.Locations.CountAsync();

    public async Task<LocationRecord?> GetByIdAsync(Guid id) =>
        await context.Locations
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

    public async Task<Guid> AddAsync(LocationRecord data)
    {
        var result = await context.Locations.AddAsync(data);
        await SaveChangesAsync();
        return result.Entity.Id;
    }

    public void Update(LocationRecord data) => context.Locations.Update(data);

    public async Task<bool> DeleteAsync(Guid id)
    {
        var record = await GetByIdAsync(id);
        context.Locations.Remove(record!);
        return await SaveChangesAsync() > 0;
    }

    public async Task<int> SaveChangesAsync() => await context.SaveChangesAsync();
}

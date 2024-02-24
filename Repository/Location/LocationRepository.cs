using Domain;
using Domain.Entities.Location;
using Microsoft.EntityFrameworkCore;

namespace Repository.Location;

internal sealed class LocationRepository : ILocationRepository
{
    private readonly HotelContext context;

    public LocationRepository(HotelContext context)
    {
        this.context = context;
    }
    public async Task<IReadOnlyCollection<LocationRecord>> GetListAsync(int offset, int limit)
    {
        return await context.Locations.Skip(offset).Take(limit).ToListAsync();
    }

    public async Task<LocationRecord?> GetByIdAsync(Guid id)
    {
        return await context.Locations
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Guid> AddAsync(LocationRecord data)
    {
        var result = await context.Locations.AddAsync(data);
        await context.SaveChangesAsync();
        return result.Entity.Id;
    }

    public async Task<bool> UpdateAsync(LocationRecord data)
    {
        context.Locations.Update(data);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var record = await GetByIdAsync(id);
        context.Locations.Remove(record!);
        return await context.SaveChangesAsync() > 0;
    }
}
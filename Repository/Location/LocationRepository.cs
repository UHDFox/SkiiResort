using Domain;
using Domain.Entities.Location;
using Microsoft.EntityFrameworkCore;

namespace Repository.Location;

internal sealed class LocationRepository : ILocationRepository
{
    private readonly SkiiResortContext _context;

    public LocationRepository(SkiiResortContext context)
    {
        this._context = context;
    }
    public async Task<IReadOnlyCollection<LocationRecord>> GetListAsync(int offset, int limit)
    {
        return await _context.Locations.Skip(offset).Take(limit).ToListAsync();
    }

    public async Task<LocationRecord?> GetByIdAsync(Guid id)
    {
        return await _context.Locations
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Guid> AddAsync(LocationRecord data)
    {
        var result = await _context.Locations.AddAsync(data);
        await SaveChangesAsync();
        return result.Entity.Id;
    }

    public async void UpdateAsync(LocationRecord data)
    {
        _context.Locations.Update(data);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var record = await GetByIdAsync(id);
        _context.Locations.Remove(record!);
        return await SaveChangesAsync() > 0;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
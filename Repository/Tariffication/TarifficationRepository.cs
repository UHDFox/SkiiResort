using Domain;
using Domain.Entities.Tariffication;
using Microsoft.EntityFrameworkCore;

namespace Repository.Tariffication;

internal sealed class TarifficationRepository : ITarifficationRepository
{
    private readonly SkiiResortContext _context;


    public TarifficationRepository(SkiiResortContext context)
    {
        this._context = context;
    }
    
    public async Task<IReadOnlyCollection<TarifficationRecord>> GetListAsync(int offset, int limit)
    {
        return await _context.Tariffications.Skip(offset).Take(limit).ToListAsync();
    }

    public async Task<TarifficationRecord?> GetByIdAsync(Guid id)
    {
        return await _context.Tariffications
            .AsNoTracking()
            .Include(x => x.Location)
            .Include(x => x.Tariff)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Guid> AddAsync(TarifficationRecord data)
    {
        var result = await _context.Tariffications.AddAsync(data);
        await SaveChangesAsync();
        return result.Entity.Id;
    }

    public void UpdateAsync(TarifficationRecord data)
    {
        _context.Tariffications.Update(data);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        _context.Tariffications.Remove((await GetByIdAsync(id))!);
        return await SaveChangesAsync() > 0;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
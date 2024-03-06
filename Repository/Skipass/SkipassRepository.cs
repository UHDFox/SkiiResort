using Domain;
using Domain.Entities.Skipass;
using Microsoft.EntityFrameworkCore;
using Repository.DbContextRepository;

namespace Repository.Skipass;

internal sealed class SkipassRepository : ISkipassRepository
{
    private readonly IDbContextRepository<HotelContext> dbContextRepository;


    public SkipassRepository(IDbContextRepository<HotelContext> dbContextRepository)
    {
        this.dbContextRepository = dbContextRepository;
    }

    public async Task<IReadOnlyCollection<SkipassRecord>> GetListAsync(int offset, int limit)
    {
        return await dbContextRepository.GetDbContext().Skipasses.Skip(offset).Take(limit).ToListAsync();
    }

    public async Task<SkipassRecord?> GetByIdAsync(Guid skipassId)
    {
        return await dbContextRepository.GetDbContext().Skipasses
            .AsNoTracking()
            .Include(record => record.Tariff)
            .Include(record => record.Visitor)
            .FirstOrDefaultAsync(x => x.Id == skipassId);
    }

    public async Task<Guid> AddAsync(SkipassRecord data)
    {
        var result = await dbContextRepository.GetDbContext().Skipasses.AddAsync(data);
        await SaveChangesAsync();
        return result.Entity.Id;
    }

    public async Task<bool> UpdateAsync(SkipassRecord data)
    {
        dbContextRepository.GetDbContext().Skipasses.Update(data);
        return await SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        dbContextRepository.GetDbContext().Skipasses.Remove((await GetByIdAsync(id))!);
        return await SaveChangesAsync() > 0;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await dbContextRepository.GetDbContext().SaveChangesAsync();
    }
}
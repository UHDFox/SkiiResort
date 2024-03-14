using Domain;
using Domain.Entities.Visitor;
using Microsoft.EntityFrameworkCore;
using Repository.DbContextRepository;

namespace Repository.Visitor;

internal sealed class VisitorRepository : IVisitorRepository
{
    private readonly IDbContextRepository<SkiiResortContext> dbContextRepository;
    
    public VisitorRepository(IDbContextRepository<SkiiResortContext> dbContextRepository)
    {
        this.dbContextRepository = dbContextRepository;
    }

    public async Task<IReadOnlyCollection<VisitorRecord>> GetListAsync(int offset, int limit)
    {
        return await dbContextRepository.GetDbContext().Visitors.Skip(offset).Take(limit).ToListAsync();
    }

    public async Task<VisitorRecord?> GetByIdAsync(Guid id)
    {
        return await dbContextRepository.GetDbContext().Visitors.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Guid> AddAsync(VisitorRecord data)
    {
        await dbContextRepository.GetDbContext().Visitors.AddAsync(data);
        await SaveChangesAsync();
        return data.Id;
    }

    public async Task<bool> UpdateAsync(VisitorRecord data)
    {
        dbContextRepository.GetDbContext().Visitors.Update(data);
        return await SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        dbContextRepository.GetDbContext().Visitors.Remove((await GetByIdAsync(id))!);
        return await SaveChangesAsync() > 0;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await dbContextRepository.GetDbContext().SaveChangesAsync();
    }
}
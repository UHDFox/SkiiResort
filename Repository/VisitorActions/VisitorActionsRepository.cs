using Domain;
using Domain.Entities.VisitorsAction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Repository.DbContextRepository;

namespace Repository.VisitorActions;

internal sealed class VisitorActionsRepository : IVisitorActionsRepository
{
    private readonly IDbContextRepository<HotelContext> dbContextRepository;

    public VisitorActionsRepository(IDbContextRepository<HotelContext> dbContextRepository)
    {
        this.dbContextRepository = dbContextRepository;
    }

    public async Task<IReadOnlyCollection<VisitorActionsRecord>> GetListAsync(int offset, int limit)
    {
        return await dbContextRepository.GetDbContext().VisitorActions.Skip(offset).Take(limit).ToListAsync();
    }

    public async Task<VisitorActionsRecord?> GetByIdAsync(Guid id)
    {
        return await dbContextRepository.GetDbContext().VisitorActions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Guid> AddAsync(VisitorActionsRecord data)
    {
        await dbContextRepository.GetDbContext().VisitorActions.AddAsync(data);
        await SaveChangesAsync();
        return data.Id;
    }

    public async Task<bool> UpdateAsync(VisitorActionsRecord data)
    {
        dbContextRepository.GetDbContext().VisitorActions.Update(data);
        return await SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        dbContextRepository.GetDbContext().VisitorActions.Remove((await GetByIdAsync(id))!);
        return await SaveChangesAsync() > 0;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await dbContextRepository.GetDbContext().SaveChangesAsync();
    }

    public async Task<IDbContextTransaction> BeginTransaction()
    {
        return await dbContextRepository.GetDbContext().Database.BeginTransactionAsync();
    }
}
using Microsoft.EntityFrameworkCore;
using SkiiResort.Domain;
using SkiiResort.Domain.Entities;

namespace SkiiResort.Repository;

public class Repository<T> : IRepository<T> where T : DataObject
{
    protected SkiiResortContext Context;

    public Repository(SkiiResortContext context)
    {
        Context = context;
    }

    public async Task<IReadOnlyCollection<T>> GetAllAsync(int offset, int limit) =>
        await Context.Set<T>().Skip(offset).Take(limit).ToListAsync();

    public async Task<int> GetTotalAmountAsync() => await Context.Set<T>().CountAsync();

    public async Task<T?> GetByIdAsync(Guid id) =>
        await Context.Set<T>().AsNoTracking().FirstOrDefaultAsync(record => record.Id == id);

    public async Task<Guid> AddAsync(T data)
    {
        await Context.Set<T>().AddAsync(data);
        await SaveChangesAsync();
        return data.Id;
    }

    public void UpdateAsync(T data) => Context.Set<T>().Update(data);

    public async Task<bool> DeleteAsync(Guid id)
    {
        Context.Set<T>().Remove((await GetByIdAsync(id))!);
        return await SaveChangesAsync() > 0;
    }

    public async Task<int> SaveChangesAsync() => await Context.SaveChangesAsync();
}

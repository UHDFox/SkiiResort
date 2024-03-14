namespace Repository;

public interface IRepository<TEntity>
{
    Task<IReadOnlyCollection<TEntity>> GetListAsync(int offset, int limit);
    
    Task<TEntity?> GetByIdAsync(Guid id);
    
    Task<Guid> AddAsync(TEntity data);
    
    void UpdateAsync(TEntity data);
    
    Task<bool> DeleteAsync(Guid id);

    Task<int> SaveChangesAsync();
}
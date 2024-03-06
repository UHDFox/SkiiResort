namespace Repository.DbContextRepository;

public interface IDbContextRepository<T> 
{
    public T GetDbContext();
}
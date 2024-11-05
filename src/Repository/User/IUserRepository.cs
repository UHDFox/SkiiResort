using SkiiResort.Domain.Entities.User;

namespace SkiiResort.Repository.User;

public interface IUserRepository : IRepository<UserRecord>
{
    public Task<UserRecord?> GetByEmailAsync(string email);
}

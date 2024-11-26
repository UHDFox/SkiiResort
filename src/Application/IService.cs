using SkiiResort.Application.User;

namespace Application;

public interface IService<TModel> where TModel : ServiceModel
{
    Task<Guid> AddAsync(TModel userModel);

    public Task<IReadOnlyCollection<TModel>> GetListAsync(int offset, int limit);

    public Task<TModel> GetByIdAsync(Guid id);

    public Task<bool> DeleteAsync(Guid id);

    public Task<TModel> UpdateAsync(TModel userModel);

    public Task<int> SaveChangesAsync();
}

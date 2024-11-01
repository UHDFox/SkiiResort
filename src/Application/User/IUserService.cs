namespace SkiiResort.Application.User;

public interface IUserService
{
    Task<IReadOnlyCollection<GetUserModel>> GetAllAsync(int offset, int limit);

    Task<GetUserModel> GetByIdAsync(Guid id);

    Task<Guid> AddAsync(AddUserModel userModel);

    Task<bool> DeleteAsync(Guid id);

    Task<UpdateUserModel> UpdateAsync(UpdateUserModel userModel);
}

using AutoMapper;
using SkiiResort.Application.Exceptions;
using SkiiResort.Domain.Entities.User;
using SkiiResort.Repository.User;

namespace SkiiResort.Application.User;

internal sealed class UserService : IUserService
{
    private readonly IMapper mapper;
    private readonly IUserRepository repository;

    public UserService(IMapper mapper, IUserRepository repository)
    {
        this.mapper = mapper;
        this.repository = repository;
    }

    public async Task<GetUserModel> GetByIdAsync(Guid id)
    {
        var user = await repository.GetByIdAsync(id) ?? throw new NotFoundException();
        return mapper.Map<GetUserModel>(user);
    }

    public async Task<IReadOnlyCollection<GetUserModel>> GetAllAsync(int offset, int limit)
    {
        var totalAmount = await repository.GetTotalAmountAsync();

        return mapper.Map<IReadOnlyCollection<GetUserModel>>(await repository.GetAllAsync(offset, limit));
    }

    public async Task<Guid> AddAsync(AddUserModel userModel)
    {
        var result = await repository.AddAsync(mapper.Map<UserRecord>(userModel));
        return result;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        await GetByIdAsync(id);
        return await repository.DeleteAsync(id);
    }

    public async Task<UpdateUserModel> UpdateAsync(UpdateUserModel userModel)
    {
        var entity = await repository.GetByIdAsync(userModel.Id)
                     ?? throw new NotFoundException("user entity not found");

        mapper.Map(userModel, entity);


        repository.Update(entity);
        await repository.SaveChangesAsync();

        return userModel;
    }
}

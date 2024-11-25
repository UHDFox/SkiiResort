using AutoMapper;
using Microsoft.AspNetCore.Http;
using SkiiResort.Application.Exceptions;
using SkiiResort.Application.Infrastructure.Authentication;
using SkiiResort.Domain.Entities.User;
using SkiiResort.Domain.Enums;
using SkiiResort.Repository.User;

namespace SkiiResort.Application.User;

internal sealed class UserService : IUserService
{
    private readonly IMapper mapper;
    private readonly IUserRepository repository;
    private readonly IJwtProvider jwtProvider;
    private readonly IPasswordProvider passwordProvider;

    public UserService(IMapper mapper, IUserRepository repository, IJwtProvider jwtProvider, IPasswordProvider passwordProvider)
    {
        this.mapper = mapper;
        this.repository = repository;
        this.jwtProvider = jwtProvider;
        this.passwordProvider = passwordProvider;
    }

    public async Task<string> LoginAsync(LoginModel model, HttpContext context)
    {
        var user = await repository.GetByEmailAsync(model.Email)
                   ?? throw new Exception("can't login - user with stated mail not found");

        if (!passwordProvider.Verify(model.Password, user.PasswordHash))
        {
            throw new LoginException("Password mismatch");
        }

        var token = jwtProvider.GenerateToken(user);
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddMinutes(20)
        };

        context.Response.Cookies.Append("some-cookie", token, cookieOptions);

        return token;
    }

    public async Task<Guid> RegisterAsync(RegisterModel model)
    {

        var hashedPassword = passwordProvider.Generate(model.Password);

        var entity = new UserRecord(model.Name, hashedPassword, model.Email, UserRole.User, DateTime.Now);

        return await AddAsync(mapper.Map<AddUserModel>(entity));


    }
    public async Task<GetUserModel> GetByIdAsync(Guid id)
    {
        var user = await repository.GetByIdAsync(id) ?? throw new NotFoundException();
        return mapper.Map<GetUserModel>(user);
    }

    public async Task<IReadOnlyCollection<GetUserModel>> GetAllAsync(int offset, int limit)
    {
        return mapper.Map<IReadOnlyCollection<GetUserModel>>(await repository.GetAllAsync(offset, limit));
    }

    public async Task<Guid> AddAsync(AddUserModel userModel)
    {
        var entity = mapper.Map<UserRecord>(userModel);

        entity.PasswordHash = passwordProvider.Generate(userModel.Password);

        var result = await repository.AddAsync(entity);

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


        repository.UpdateAsync(entity);
        await repository.SaveChangesAsync();

        return userModel;
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Http;
using SkiiResort.Application.Exceptions;
using SkiiResort.Application.Infrastructure.Authentication;
using SkiiResort.Domain.Entities.User;
using SkiiResort.Domain.Enums;
using SkiiResort.Repository.User;

namespace SkiiResort.Application.User;

internal sealed class UserService : Service<UserModel, UserRecord>, IUserService
{
    private readonly IMapper mapper;
    private readonly IUserRepository repository;
    private readonly IJwtProvider jwtProvider;
    private readonly IPasswordProvider passwordProvider;

    public UserService(IMapper mapper, IUserRepository repository, IJwtProvider jwtProvider, IPasswordProvider passwordProvider)
    : base(repository, mapper)
    {
        this.mapper = mapper;
        this.repository = repository;
        this.jwtProvider = jwtProvider;
        this.passwordProvider = passwordProvider;
    }

    public new async Task<Guid> AddAsync(UserModel userModel)
    {
        var entity = mapper.Map<UserRecord>(userModel);

        entity.PasswordHash = passwordProvider.Generate(userModel.Password);

        var result = await repository.AddAsync(entity);

        return result;
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

        return await AddAsync(mapper.Map<UserModel>(entity));
    }

    public new async Task<UserModel> UpdateAsync(UserModel userModel)
    {
        var entity = await repository.GetByIdAsync(userModel.Id)
                     ?? throw new NotFoundException("user entity not found");

        mapper.Map(userModel, entity);

        entity.PasswordHash = passwordProvider.Generate(userModel.Password);

        repository.UpdateAsync(entity);
        await repository.SaveChangesAsync();

        return userModel;
    }
}

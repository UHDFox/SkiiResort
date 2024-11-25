using Application;
using Microsoft.AspNetCore.Http;

namespace SkiiResort.Application.User;

public interface IUserService : IService<UserModel>
{
    Task<string> LoginAsync(LoginModel model, HttpContext context);

    Task<Guid> RegisterAsync(RegisterModel model);
}

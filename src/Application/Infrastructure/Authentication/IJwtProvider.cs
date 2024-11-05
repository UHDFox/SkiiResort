using SkiiResort.Domain.Entities.User;

namespace SkiiResort.Web.Infrastructure;

public interface IJwtProvider
{
    public string GenerateToken(UserRecord user);
}

using SkiiResort.Domain.Entities.User;

namespace SkiiResort.Application.Infrastructure.Authentication;

public interface IJwtProvider
{
    public string GenerateToken(UserRecord user);
}

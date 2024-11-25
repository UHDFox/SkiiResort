using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace SkiiResort.Application.Infrastructure.Authentication;

public static class AuthOptions
{
    public const string Issuer = "CapybaraSkiiResort";
    public const string Audience = "SkiiResort";
    private const string Key = "testSecretKeyLoooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooong"; // encryption key

    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
    }
}

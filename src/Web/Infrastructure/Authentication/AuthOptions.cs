using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace SkiiResort.Web.Infrastructure;

public class AuthOptions
{
    public const string Issuer = "CapybaraSkiiResort";
    public const string Audience = "SkiiResort";
    private const string Key = "testSecretKey"; // encryption key

    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
    }
}

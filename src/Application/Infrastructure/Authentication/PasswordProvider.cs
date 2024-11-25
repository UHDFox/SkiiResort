using System.Security.Cryptography;
using System.Text;

namespace SkiiResort.Application.Infrastructure.Authentication;

public sealed class PasswordProvider : IPasswordProvider
{
    public string Generate(string password)
    {
        return CalculateSha256(password);
    }

    public bool Verify(string password, string hashedPassword)
    {
        var curHash = CalculateSha256(password);

        return string.Equals(curHash, hashedPassword);
    }

    private string CalculateSha256(string password)
    {
        using (var hasher = SHA256.Create())
        {
            byte[] hashValue = hasher.ComputeHash(Encoding.UTF8.GetBytes(password));


            return BitConverter.ToString(hashValue).Replace("-", "").ToLowerInvariant();
        }
    }
}

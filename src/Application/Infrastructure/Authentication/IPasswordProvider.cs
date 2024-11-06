namespace SkiiResort.Application.Infrastructure.Authentication;

public interface IPasswordProvider
{
    public string Generate(string password);

    public bool Verify(string password, string hashedPassword);
}

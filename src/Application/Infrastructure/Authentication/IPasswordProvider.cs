namespace SkiiResort.Web.Infrastructure;

public interface IPasswordProvider
{
    public string Generate(string password);

    public bool Verify(string password, string hashedPassword);
}

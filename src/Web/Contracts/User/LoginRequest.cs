namespace SkiiResort.Web.Contracts.User;

public sealed class LoginRequest
{
    public LoginRequest(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public string Email { get; set; }

    public string Password { get; set; }
}

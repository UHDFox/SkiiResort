namespace SkiiResort.Web.Contracts.User.Requests;

public sealed class RegisterRequest
{
    public RegisterRequest(string name, string password, string email)
    {
        Name = name;
        Password = password;
        Email = email;
    }

    public string Name { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }
}

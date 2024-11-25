using SkiiResort.Domain.Enums;

namespace SkiiResort.Application.User;

public sealed class AddUserModel
{
    public AddUserModel(string name, string password, string email, UserRole role, DateTimeOffset createdAt)
    {
        Name = name;
        Password = password;
        Email = email;
        Role = role;
        CreatedAt = createdAt;
    }

    public string Name { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }

    public UserRole Role { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}

using Application;
using SkiiResort.Domain.Enums;

namespace SkiiResort.Application.User;

public sealed class UserModel : ServiceModel
{
    public UserModel(Guid id, string name, string email, string password, UserRole role, DateTimeOffset createdAt)
        : base(id)
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
        Role = role;
        CreatedAt = createdAt;
    }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public UserRole Role { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}

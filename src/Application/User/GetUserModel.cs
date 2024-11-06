using SkiiResort.Domain.Enums;

namespace SkiiResort.Application.User;

public sealed class GetUserModel
{
    public GetUserModel(Guid id, string name, string email, string passwordHash, UserRole role, DateTimeOffset createdAt)
    {
        Id = id;
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        CreatedAt = createdAt;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public UserRole Role { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}

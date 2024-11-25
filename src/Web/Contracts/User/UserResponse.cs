using SkiiResort.Domain.Enums;

namespace SkiiResort.Web.Contracts.User;

public sealed class UserResponse
{
    public UserResponse(Guid id, string name, string passwordHash, string email, UserRole role, DateTimeOffset createdAt)
    {
        Id = id;
        Name = name;
        PasswordHash = passwordHash;
        Role = role;
        CreatedAt = createdAt;
        Email = email;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string PasswordHash { get; set; }

    public string Email { get; set; }

    public UserRole Role { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}

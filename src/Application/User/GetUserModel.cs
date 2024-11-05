using SkiiResort.Domain.Enums;

namespace SkiiResort.Application.User;

public class GetUserModel
{
    public GetUserModel(Guid id, string name, string email, string passwordHash, UserRole role, DateTime createdAt, Guid visitorId)
    {
        Id = id;
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        CreatedAt = createdAt;
        VisitorId = visitorId;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public UserRole Role { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid VisitorId { get; set; }
}

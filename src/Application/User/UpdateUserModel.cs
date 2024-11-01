using SkiiResort.Domain.Enums;

namespace SkiiResort.Application.User;

public class UpdateUserModel
{
    public UpdateUserModel(string name, string passwordHash, UserRole role, DateTime createdAt, Guid visitorId)
    {
        Name = name;
        PasswordHash = passwordHash;
        Role = role;
        CreatedAt = createdAt;
        VisitorId = visitorId;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string PasswordHash { get; set; }

    public UserRole Role { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid VisitorId { get; set; }
}

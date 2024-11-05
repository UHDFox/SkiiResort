using SkiiResort.Domain.Enums;

namespace SkiiResort.Application.User;

public class UpdateUserModel
{
    public UpdateUserModel(string name, string password, string email, UserRole role, DateTime createdAt, Guid visitorId)
    {
        Name = name;
        Password = password;
        Email = email;
        Role = role;
        CreatedAt = createdAt;
        VisitorId = visitorId;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public UserRole Role { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid VisitorId { get; set; }
}

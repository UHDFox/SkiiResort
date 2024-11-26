using SkiiResort.Domain.Enums;

namespace SkiiResort.Web.Contracts.User.Requests;

public sealed class UpdateUserRequest
{
    public UpdateUserRequest(string name, string email, string password, UserRole role, DateTimeOffset createdAt)
    {
        Name = name;
        Password = password;
        Email = email;
        Role = role;
        CreatedAt = createdAt;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public UserRole Role { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}

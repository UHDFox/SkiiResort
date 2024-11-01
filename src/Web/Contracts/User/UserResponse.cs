﻿using SkiiResort.Domain.Enums;

namespace SkiiResort.Web.Contracts.User;

public sealed class UserResponse
{
    public UserResponse(Guid id, string name, string passwordHash, UserRole role, DateTime createdAt, Guid visitorId)
    {
        Id = id;
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

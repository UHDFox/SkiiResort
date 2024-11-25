﻿using SkiiResort.Domain.Enums;

namespace SkiiResort.Web.Contracts.User.Requests;

public sealed class UpdateUserRequest
{
    public UpdateUserRequest(string name, string passwordHash, string email, UserRole role, DateTimeOffset createdAt)
    {
        Name = name;
        PasswordHash = passwordHash;
        Email = email;
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
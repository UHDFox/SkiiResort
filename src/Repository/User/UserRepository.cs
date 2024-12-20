﻿using Microsoft.EntityFrameworkCore;
using SkiiResort.Domain;
using SkiiResort.Domain.Entities.User;

namespace SkiiResort.Repository.User;

internal sealed class UserRepository : Repository<UserRecord>, IUserRepository
{
    private readonly SkiiResortContext context;

    public UserRepository(SkiiResortContext context) : base(context)
    {
        this.context = context;
    }

    public Task<UserRecord?> GetByEmailAsync(string email)
        => context.Users.FirstOrDefaultAsync(e => e.Email == email);

    public new void UpdateAsync(UserRecord data)
    {
        context.Entry(data).Property(x => x.PasswordHash).IsModified = true;
        Context.Users.Update(data);
    }
}

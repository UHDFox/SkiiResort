﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using SkiiResort.Application.Exceptions;
using SkiiResort.Domain.Entities.User;
using SkiiResort.Repository.User;
using SkiiResort.Web.Infrastructure;

namespace SkiiResort.Application.User;

internal sealed class UserService : IUserService
{
    private readonly IMapper mapper;
    private readonly IUserRepository repository;
    private readonly IJwtProvider jwtProvider;
    private readonly IPasswordProvider passwordProvider;

    public UserService(IMapper mapper, IUserRepository repository, IJwtProvider jwtProvider, IPasswordProvider passwordProvider)
    {
        this.mapper = mapper;
        this.repository = repository;
        this.jwtProvider = jwtProvider;
        this.passwordProvider = passwordProvider;
    }

    public async Task<string> LoginAsync(LoginModel model, HttpContext context)
    {
        var user = await repository.GetByEmailAsync(model.Email)
                   ?? throw new Exception("can't login - user with stated mail not found");

        if (!passwordProvider.Verify(model.Password, user.PasswordHash))
        {
            throw new LoginException("Password mismatch");
        }

        var token = jwtProvider.GenerateToken(user);

        context.Response.Cookies.Append("some-cookie", token);

        return token;
    }

    public async Task<GetUserModel> GetByIdAsync(Guid id)
    {
        var user = await repository.GetByIdAsync(id) ?? throw new NotFoundException();
        return mapper.Map<GetUserModel>(user);
    }

    public async Task<IReadOnlyCollection<GetUserModel>> GetAllAsync(int offset, int limit)
    {
        var totalAmount = await repository.GetTotalAmountAsync();

        return mapper.Map<IReadOnlyCollection<GetUserModel>>(await repository.GetAllAsync(offset, limit));
    }

    public async Task<Guid> AddAsync(AddUserModel userModel)
    {
        var entity = mapper.Map<UserRecord>(userModel);

        entity.PasswordHash = passwordProvider.Generate(userModel.Password);

        var result = await repository.AddAsync(entity);

        return result;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        await GetByIdAsync(id);
        return await repository.DeleteAsync(id);
    }

    public async Task<UpdateUserModel> UpdateAsync(UpdateUserModel userModel)
    {
        var entity = await repository.GetByIdAsync(userModel.Id)
                     ?? throw new NotFoundException("user entity not found");

        mapper.Map(userModel, entity);


        repository.Update(entity);
        await repository.SaveChangesAsync();

        return userModel;
    }
}

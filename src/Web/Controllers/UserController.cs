using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SkiiResort.Application.User;
using SkiiResort.Domain.Entities.User;
using SkiiResort.Web.Contracts.CommonResponses;
using SkiiResort.Web.Contracts.User;

namespace SkiiResort.Web.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public sealed class UserController : Controller
{
    private readonly IUserService userService;
    private readonly IMapper mapper;

    public UserController(IUserService userService, IMapper mapper)
    {
        this.userService = userService;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllResponse<UserRecord>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetListAsync(int? offset, int? limit)
    {
        var result = mapper.Map<IReadOnlyCollection<UserResponse>>
            (await userService.GetAllAsync(offset.GetValueOrDefault(0), limit.GetValueOrDefault(5)));

        return Ok(new GetAllResponse<UserResponse>(result, result.Count));
    }

    [HttpGet("id:guid")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserRecord))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id) => Ok(mapper.Map<UserResponse>(await userService.GetByIdAsync(id)));

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreatedResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddAsync(CreateUserRequest data)
    {
        var result = await userService.AddAsync(mapper.Map<AddUserModel>(data));
        return Created($"{Request.Path}", mapper.Map<UserResponse>(await userService.GetByIdAsync(result)));
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdatedResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAsync(UpdateUserRequest data)
    {
        await userService.UpdateAsync(mapper.Map<UpdateUserModel>(data));
        return Ok(new UpdatedResponse(data.Id));
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeletedResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var result = await userService.DeleteAsync(id);
        return Ok(new DeletedResponse(id, result));
    }
}

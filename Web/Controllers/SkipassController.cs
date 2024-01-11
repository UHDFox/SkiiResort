using Application.Exceptions;
using Application.Skipass;
using AutoMapper;
using Domain.Entities.Skipass;
using Microsoft.AspNetCore.Mvc;
using Web.Contracts.CommonResponses;
using Web.Contracts.Skipass;

namespace Web.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public sealed class SkipassController : Controller
{
    private readonly ISkipassService context;
    private readonly IMapper mapper;

    public SkipassController(ISkipassService context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    [HttpGet(Name = "Get skipasses")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllResponse<SkipassResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetListAsync(int offset, int limit)
    {
        var result = await context.GetListAsync(offset, limit);
        return Ok(new GetAllResponse<SkipassResponse>(mapper.Map<IReadOnlyCollection<SkipassResponse>>(result),
            result.Count));
    }

    [HttpGet(Name = "Get skipass by Id")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SkipassResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await context.GetByIdAsync(id) ?? throw new NotFoundException("Tariff not found");
        return Ok(mapper.Map<SkipassRecord>(result));
    }

    [HttpPost(Name = "Create skipass")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreatedResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    public async Task<IActionResult> AddAsync(Guid id, AddSkipassModel skipassModel)
    {
        var result = await context.AddAsync(skipassModel);

        return Created($"{Request.Path}", mapper.Map<SkipassResponse>(result));
    }

    [HttpPut(Name = "Update record")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdatedResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAsync(Guid id, UpdateSkipassModel skipassModel)
    {
        var record = await context.GetByIdAsync(id);
        var updatedRecord = mapper.Map<UpdateSkipassModel>(skipassModel);
        var result = await context.UpdateAsync(updatedRecord);
        return Ok(new UpdatedResponse(id, result));
    }

    [HttpDelete(Name = "Delete skipass record")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeletedResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(Guid id, DeleteSkipassModel skipassModel)
    {
        var record = context.GetByIdAsync(id);
        if (record == null) NotFound();

        var result = await context.DeleteAsync(skipassModel);
        return Ok(new DeletedResponse(id, result));
    }
}
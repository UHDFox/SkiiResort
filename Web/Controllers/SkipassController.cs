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
    public async Task<IActionResult> GetListAsync(int? offset, int? limit)
    {
        var result = await context.GetListAsync();
        return Ok(new GetAllResponse<SkipassResponse>(mapper.Map<IReadOnlyCollection<SkipassResponse>>(result),
            result.Count).List);
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
    public async Task<IActionResult> AddAsync(AddSkipassModel skipassModel)
    {
        var result = await context.AddAsync(skipassModel);

        return Created($"{Request.Path}", mapper.Map<SkipassResponse>(result));
    }

    [HttpPut(Name = "Update record")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdatedResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAsync(UpdateSkipassModel skipassModel)
    {
        /*var record = await context.GetByIdAsync(skipassModel.Id);
        var updatedRecord = mapper.Map<UpdateSkipassModel>(skipassModel);
        var result = await context.UpdateAsync(updatedRecord);
        return Ok(new UpdatedResponse(updatedRecord.Id, result));*/
        var result = await context.UpdateAsync(skipassModel);
        return Ok(new UpdatedResponse(skipassModel.Id, result));
    }

    [HttpDelete(Name = "Delete skipass record")]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(DeletedResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var result = await context.DeleteAsync(id);
        return NoContent();
    }
}
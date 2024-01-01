using Application.Exceptions;
using Application.Tariff;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web.Contracts.CommonResponses;
using Web.Contracts.Tariff;

namespace Web.Controllers;

[Route("api/[action]")]
[ApiController]
public class TariffController : Controller
{
    private readonly ITariffService context;
    private readonly IMapper mapper;

    public TariffController(ITariffService context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    [HttpPost("Create tariff")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreatedResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    public async Task<IActionResult> AddAsync(AddTariffModel tariffModel)
    {
        var result = await context.AddAsync(tariffModel);
        return Created($"{Request.Path}", result);
    }

    [HttpGet("Get tariffs list")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllResponse<TariffResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetListAsync(int offset, int limit)
    {
        var result = await context.GetListAsync(offset = 0, limit = 150);
        return Ok(new GetAllResponse<TariffResponse>(mapper.Map<IReadOnlyCollection<TariffResponse>>(result),
            result.Count).List);
    }

    [HttpGet("Get tariff by Id")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TariffResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<GetTariffModel> GetByIdAsync(Guid id)
    {
        var result = await context.GetByIdAsync(id) ?? throw new NotFoundException("There's no record with such an ID");
        return result;
    }
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(DeletedResponse))]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await context.DeleteAsync(id);
        return NoContent();
    }

    [HttpPut("Update tariff")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdatedResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAsync(Guid id, UpdateTariffModel tariffModel)
    {
        var result = await context.UpdateAsync(id, tariffModel);
        return Ok(new UpdatedResponse(id, result));
    }
}
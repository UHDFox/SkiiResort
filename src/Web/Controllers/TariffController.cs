using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SkiiResort.Application.Tariff;
using SkiiResort.Web.Contracts.CommonResponses;
using SkiiResort.Web.Contracts.Tariff;
using SkiiResort.Web.Contracts.Tariff.Requests;

namespace SkiiResort.Web.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public sealed class TariffController : Controller
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
    public async Task<IActionResult> AddAsync(CreateTariffRequest tariffModel)
    {
        var id = await context.AddAsync(mapper.Map<AddTariffModel>(tariffModel));
        return Created($"{Request.Path}", mapper.Map<TariffResponse>(await context.GetByIdAsync(id)));
    }

    [HttpGet("Get tariffs list")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllResponse<TariffResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetListAsync(int? offset, int? limit)
    {
        var collection =
            mapper.Map<IReadOnlyCollection<TariffResponse>>(await context.GetListAsync(offset.GetValueOrDefault(0),
                limit.GetValueOrDefault(15)));
        return Ok(new GetAllResponse<TariffResponse>(collection, collection.Count));
    }

    [HttpGet("Get tariff by Id")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TariffResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await context.GetByIdAsync(id);
        return Ok(mapper.Map<TariffResponse>(result));
    }

    [HttpPut("Update tariff")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdatedResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAsync(UpdateTariffRequest tariffModel)
    {
        await context.UpdateAsync(mapper.Map<UpdateTariffModel>(tariffModel));
        return Ok(new UpdatedResponse(tariffModel.Id));
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeletedResponse))]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var result = await context.DeleteAsync(id);
        return Ok(new DeletedResponse(id, result));
    }
}

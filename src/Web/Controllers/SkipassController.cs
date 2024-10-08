using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SkiiResort.Application.Skipass;
using SkiiResort.Web.Contracts.CommonResponses;
using SkiiResort.Web.Contracts.Skipass;
using SkiiResort.Web.Contracts.Skipass.Requests;

namespace SkiiResort.Web.Controllers;

[Route("api/v1/[controller]")]
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

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllResponse<SkipassResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetListAsync(int? offset, int? limit)
    {
        var result = await context.GetListAsync(offset.GetValueOrDefault(0), limit.GetValueOrDefault(5));
        return Ok(new GetAllResponse<SkipassResponse>(mapper.Map<IReadOnlyCollection<SkipassResponse>>(result),
            result.Count));
    }

    [HttpGet("id:guid")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SkipassResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await context.GetByIdAsync(id);
        return Ok(mapper.Map<SkipassResponse>(result));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SkipassResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    public async Task<IActionResult> AddAsync(CreateSkipassRequest skipassModel)
    {
        var id = await context.AddAsync(mapper.Map<AddSkipassModel>(skipassModel));

        return Created($"{Request.Path}", mapper.Map<SkipassResponse>(await context.GetByIdAsync(id)));
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdatedResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAsync(UpdateSkipassRequest skipassModel)
    {
        await context.UpdateAsync(mapper.Map<UpdateSkipassModel>(skipassModel));
        return Ok(new UpdatedResponse(skipassModel.Id));
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeletedResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var result = await context.DeleteAsync(id);
        return Ok(new DeletedResponse(id, result));
    }
}

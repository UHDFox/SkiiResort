using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SkiiResort.Application.Visitor;
using SkiiResort.Web.Contracts.CommonResponses;
using SkiiResort.Web.Contracts.Visitor;
using SkiiResort.Web.Contracts.Visitor.Requests;

namespace SkiiResort.Web.Controllers;

[ApiController]
[Route("api/v1/[controller]/")]
public sealed class VisitorController : Controller
{
    private readonly IMapper mapper;
    private readonly IVisitorService visitorService;

    public VisitorController(IVisitorService visitorService, IMapper mapper)
    {
        this.visitorService = visitorService;
        this.mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreatedResponse))]
    public async Task<IActionResult> AddAsync(CreateVisitorRequest model)
    {
        var id = await visitorService.AddAsync(mapper.Map<AddVisitorModel>(model));
        return Created($"{Request.Path}", mapper.Map<VisitorResponse>(await visitorService.GetByIdAsync(id)));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllResponse<VisitorResponse>))]
    public async Task<IActionResult> GetAllAsync(int? offset, int? limit)
    {
        var result = await visitorService.GetListAsync(offset.GetValueOrDefault(0), limit.GetValueOrDefault(15));
        return Ok(new GetAllResponse<VisitorResponse>(mapper.Map<IReadOnlyCollection<VisitorResponse>>(result),
            await visitorService.GetTotalAmountAsync()));
    }

    [HttpGet("id:guid")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VisitorResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await visitorService.GetByIdAsync(id);
        return Ok(mapper.Map<VisitorResponse>(result));
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdatedResponse))]
    public async Task<IActionResult> UpdateAsync(UpdateVisitorRequest model)
    {
        var result = await visitorService.UpdateAsync(mapper.Map<UpdateVisitorModel>(model));
        return Ok(new UpdatedResponse(model.Id));
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeletedResponse))]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(DeletedResponse))]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var result = await visitorService.DeleteAsync(id);
        return Ok(new DeletedResponse(id, result));
    }
}

using Application.VisitorAction;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web.Contracts;
using Web.Contracts.CommonResponses;
using Web.Contracts.VisitorActions;
using Web.Contracts.VisitorActions.Requests;

namespace Web.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public sealed class VisitorActionsController : Controller
{
    private readonly IMapper mapper;
    private readonly IVisitorActions visitorActionsService;

    public VisitorActionsController(IVisitorActions visitorActionsService, IMapper mapper)
    {
        this.visitorActionsService = visitorActionsService;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllResponse<VisitorActionsResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllAsync(int? offset, int? limit)
    {
        var result = await visitorActionsService.GetAllAsync(offset.GetValueOrDefault(0), limit.GetValueOrDefault(15));
        return Ok(new GetAllResponse<VisitorActionsResponse>(
            mapper.Map<IReadOnlyCollection<VisitorActionsResponse>>(result), result.Count));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VisitorActionsResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await visitorActionsService.GetByIdAsync(id);
        return Ok(mapper.Map<VisitorActionsResponse>(result));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreatedResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddAsync(CreateVisitorActionsRequest model)
    {
       var id = await visitorActionsService.AddAsync(mapper.Map<AddVisitorActionsModel>(model));
        return Created($"{Request.Path}",
            mapper.Map<VisitorActionsResponse>(await visitorActionsService.GetByIdAsync(id)));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreatedResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> TapSkipass(TapSkipassRequest request)
    {
        var id = await visitorActionsService.TapSkipass(mapper.Map<AddVisitorActionsModel>(request));
        return Created($"{Request.Path}",
            mapper.Map<VisitorActionsResponse>(await visitorActionsService.GetByIdAsync(id)));
    }
    

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreatedResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DepositSkipassBalance(DepositSkipassBalanceRequest request)
    {
        var id = await visitorActionsService.DepositSkipassBalance(mapper.Map<AddVisitorActionsModel>(request));
        return Created($"{Request.Path}",
            mapper.Map<VisitorActionsResponse>(await visitorActionsService.GetByIdAsync(id)));
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdatedResponse))]
    public async Task<IActionResult> UpdateAsync(UpdateVisitorActionsRequest model)
    {
        await visitorActionsService.UpdateAsync(mapper.Map<UpdateVisitorActionsModel>(model));
        return Ok(new UpdatedResponse(model.Id));
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeletedResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var result = await visitorActionsService.DeleteAsync(id);
        return Ok(new DeletedResponse(id, result));
    }
}
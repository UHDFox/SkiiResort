using System.Reflection.Metadata.Ecma335;
using Application.VisitorAction;
using AutoMapper;
using Domain.Entities.VisitorsAction;
using Microsoft.AspNetCore.Mvc;
using Web.Contracts.CommonResponses;
using Web.Contracts.Visitor;
using Web.Contracts.VisitorActions;


namespace Web.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]

public sealed class VisitorActionsController : Controller
{
    private readonly IVisitorActions visitorActionsService;
    private readonly IMapper mapper;

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
        return Ok(new GetAllResponse<VisitorActionsResponse>(mapper.Map<IReadOnlyCollection<VisitorActionsResponse>>(result), result.Count));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreatedResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Addsync(AddVisitorActionsModel model)
    {
        await visitorActionsService.AddAsync(model);
        return Created($"{Request.Path}", model);
    }
}
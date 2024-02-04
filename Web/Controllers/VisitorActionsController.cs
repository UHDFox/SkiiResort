using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Application.VisitorAction;
using AutoMapper;
using Domain.Entities.VisitorsAction;
using Microsoft.AspNetCore.Http;
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

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VisitorActionsResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await visitorActionsService.GetByIdAsync(id);
        return Ok(mapper.Map<VisitorActionsRecord>(result));
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreatedResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddAsync(AddVisitorActionsModel model)
    {
        await visitorActionsService.AddAsync(model);
        return Created($"{Request.Path}", model);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdatedResponse))]
    public async Task<IActionResult> UpdateAsync(UpdateVisitorActionsModel model)
    {
        var result = await visitorActionsService.UpdateAsync(model);
        return Ok(new UpdatedResponse(model.Id, result));
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await visitorActionsService.DeleteAsync(id);
        return NoContent();
    }
}
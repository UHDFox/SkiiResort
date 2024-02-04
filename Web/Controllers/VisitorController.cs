using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Tariff;
using Application.Visitor;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Contracts.CommonResponses;
using Web.Contracts.Visitor;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public sealed class VisitorController : Controller
{
    private readonly IVisitorService visitorService;
    private readonly IMapper mapper;
    
    public VisitorController(IVisitorService visitorService, IMapper mapper)
    {
        this.visitorService = visitorService;
        this.mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreatedResponse))]
    public async Task<IActionResult> AddAsync(AddVisitorModel model)
    {
        await visitorService.AddAsync(model);
        return Created($"{Request.Path}", model);
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllResponse<VisitorResponse>))]
    public async Task<IActionResult> GetAllAsync(int? offset, int? limit)
    {
        var result = await visitorService.GetListAsync(offset.GetValueOrDefault(0), limit.GetValueOrDefault(15));
        return Ok(new GetAllResponse<VisitorResponse>(mapper.Map<IReadOnlyCollection<VisitorResponse>>(result), result.Count));
    }
    
    [HttpGet]
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
    public async Task<IActionResult> UpdateAsync(UpdateVisitorModel model)
    {
        var result = await visitorService.UpdateAsync(model);
        return Ok(new UpdatedResponse(model.Id, result));
    }
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await visitorService.DeleteAsync(id);
        return NoContent();
    }
}
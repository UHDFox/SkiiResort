using Application.Location;
using Application.Location.Models;
using AutoMapper;
using Domain.Entities.Location;
using Microsoft.AspNetCore.Mvc;
using Web.Contracts.CommonResponses;
using Web.Contracts.Location;
using Web.Contracts.Location.Requests;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public sealed class LocationController : Controller
{
    private readonly ILocationService locationService;
    private readonly IMapper mapper;

    public LocationController(ILocationService locationService, IMapper mapper)
    {
        this.locationService = locationService;
        this.mapper = mapper;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllResponse<LocationRecord>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetListAsync(int? offset, int? limit)
    {
        var result = mapper.Map<IReadOnlyCollection<LocationResponse>>
            (await locationService.GetAllAsync(offset.GetValueOrDefault(0), limit.GetValueOrDefault(15)));
        
        return Ok(new GetAllResponse<LocationResponse>(result, result.Count));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LocationRecord))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        return Ok(mapper.Map<LocationResponse>(await locationService.GetByIdAsync(id)));
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreatedResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddAsync(CreateLocationRequest data)
    {
        var result = await locationService.AddAsync(mapper.Map<AddLocationModel>(data));
        return Created(Request.Path, await locationService.GetByIdAsync(result));
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdatedResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAsync(UpdateLocationRequest data)
    {
        var result = await locationService.UpdateAsync(mapper.Map<UpdateLocationModel>(data));
        return Ok(new UpdatedResponse(data.Id, result));
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await locationService.DeleteAsync(id);
        return NoContent();
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Tariff;
using AutoMapper;
using Domain.Entities.Tariff;
using Microsoft.AspNetCore.Http;
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
    public async Task<IActionResult> GetListAsync(int? offset, int? limit)
    {
        var collection = mapper.Map<IReadOnlyCollection<TariffResponse>>(await context.GetListAsync(offset.GetValueOrDefault(0), limit.GetValueOrDefault(15)));
        return Ok(new GetAllResponse<TariffResponse>(collection,collection.Count));
    }

    [HttpGet("Get tariff by Id")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TariffResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await context.GetByIdAsync(id);
        return Ok(mapper.Map<TariffRecord>(result));
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
    public async Task<IActionResult> UpdateAsync(UpdateTariffModel tariffModel)
    {
        var result = await context.UpdateAsync(tariffModel);
        return Ok(new UpdatedResponse(tariffModel.Id, result));
    }
}
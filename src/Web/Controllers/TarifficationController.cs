using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkiiResort.Application.Tariffication;
using SkiiResort.Application.Tariffication.Models;
using SkiiResort.Web.Contracts.CommonResponses;
using SkiiResort.Web.Contracts.Tariffication;
using SkiiResort.Web.Contracts.Tariffication.Requests;

namespace SkiiResort.Web.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public sealed class TarifficationController : Controller
{
    private readonly IMapper mapper;
    private readonly ITarifficationService tarifficationService;

    public TarifficationController(ITarifficationService tarifficationService, IMapper mapper)
    {
        this.tarifficationService = tarifficationService;
        this.mapper = mapper;
    }


    [HttpGet]
    [Authorize(Roles = "SuperAdmin, HighLevelAdmin, LowLevelAdmin")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllResponse<TarifficationResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetListAsync(int? offset, int? limit)
    {
        var result = mapper.Map<IReadOnlyCollection<TarifficationResponse>>
            (await tarifficationService.GetAllAsync(offset.GetValueOrDefault(0), limit.GetValueOrDefault(5)));

        return Ok(new GetAllResponse<TarifficationResponse>(result, result.Count));
    }

    [HttpGet("id:guid")]
    [Authorize(Roles = "SuperAdmin, HighLevelAdmin, LowLevelAdmin")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TarifficationResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id) => Ok(mapper.Map<TarifficationResponse>(await tarifficationService.GetByIdAsync(id)));

    [HttpPost]
    [Authorize(Roles = "SuperAdmin")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreatedResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddAsync(CreateTarifficationRequest request)
    {
        var id = await tarifficationService.AddAsync(mapper.Map<AddTarifficationModel>(request));
        return Created(Request.Path, await GetByIdAsync(id));
    }

    [HttpPut]
    [Authorize(Roles = "SuperAdmin")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdatedResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAsync(UpdateTarifficationRequest request)
    {
        await tarifficationService.UpdateAsync(mapper.Map<UpdateTarifficationModel>(request));
        return Ok(new UpdatedResponse(request.Id));
    }


    [HttpDelete]
    [Authorize(Roles = "SuperAdmin")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeletedResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var result = await tarifficationService.DeleteAsync(id);
        return Ok(new DeletedResponse(id, result));
    }
}

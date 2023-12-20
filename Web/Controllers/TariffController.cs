using Application.Tariff;
using AutoMapper;
using Domain;
using Domain.Entities.Tariff;
using Microsoft.AspNetCore.Mvc;
using Web.Contracts.CommonResponses;
using Web.Contracts.Tariff;

namespace Web.Controllers;



[Route("api/[controller]/[action]")]
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
    
    [HttpPost(Name = "Create tariff")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreatedResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    public async Task<IActionResult> AddAsync(Guid id, AddTariffModel tariffModel)
    {
        //var tariff = mapper.Map<AddTariffModel>(tariffModel);
        //var result = await context.AddAsync(mapper.Map<TariffRecord>(tariffModel));
        var result = await context.AddAsync(tariffModel);

        return Created($"{Request.Path}", mapper.Map<TariffResponse>(result));
    }

    [HttpGet(Name = "GetList")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllResponse<TariffResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetListAsync(int offset, int limit)
    {
        var result = await context.GetListAsync(offset, limit);
       // return Ok(new IReadOnlyCollection<GetTariffModel>(mapper.Map<IReadOnlyCollection<TariffResponse>>(result, )));
       return Ok(new GetAllResponse<TariffResponse>(mapper.Map<IReadOnlyCollection<TariffResponse>>(result),
           result.Count));
    }
}
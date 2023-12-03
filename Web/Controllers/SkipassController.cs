using Application.Skipass;
using Domain.Entities.Skipass;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("api/[controller]/[action]")]
[Controller]
public sealed class SkipassController : Controller
{
    private readonly ISkipassService context;

    public SkipassController(ISkipassService context)
    {
        this.context = context;
    }

    [HttpGet]
    public IReadOnlyCollection<SkipassRecord> ShowAllSkipasses()
    {
        var skipasses = context.GetListAsync();
        return skipasses;
    }

    [HttpGet]
    public async Task<SkipassDto> GetSkipassById(int skipassId)
    {
        var result = await context.GetSkipassById(skipassId);
        return result;
    }

    [HttpPost]
    public async Task<SkipassDto> AddNewSkipass(SkipassDto request)
    {
        var result = await context.AddNewSkipass(request);
        return result;
    }

    [HttpPut]
    public async Task<SkipassDto> UpdateSkipassInfo(SkipassDto updatedSkipass)
    {
        var result = await context.UpdateSkipassInfo(updatedSkipass);
        return result;
    }

    [HttpDelete]
    public async Task<SkipassDto> DeleteSkipass(int skipassId)
    {
        var result = await context.DeleteSkipass(skipassId);
        return result;
    }
}
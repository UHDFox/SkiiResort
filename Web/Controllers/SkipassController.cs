using Application.Entities;
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
    /*List<SkipassDto> ShowAllSkipasses();
    Task<SkipassDto> GetSkipassByID(int skipassId);
    Task<SkipassDto> AddNewSkipass(SkipassDto request);
    Task<SkipassDto> UpdateSkipassInfo(SkipassDto updatedSkipass);
    Task<SkipassDto> DeleteSkipass(int skipassId);*/
    [HttpGet]
    public List<SkipassDto> ShowAllSkipasses()
    {
        var skipasses = context.ShowAllSkipasses();
        return skipasses;
    }

    [HttpGet]
    public async Task<SkipassDto> GetSkipassByID(int skipassId)
    {
         var result = await context.GetSkipassByID(skipassId);
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
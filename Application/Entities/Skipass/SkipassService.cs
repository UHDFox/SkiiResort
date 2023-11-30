using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Entities;

public class SkipassService : ISkipassService 
{
    private readonly IDBContext context;
    private readonly IMapper mapper;

    public SkipassService(IDBContext context, IMapper mapper) 
    {
        this.context = context;
        this.mapper = mapper;
    }
    
    public List<SkipassDto> ShowAllSkipasses()
    {
        return (context.Skipasses.ToList().Select(x => mapper.Map<SkipassDto>(x)).ToList());
    }

    public async Task<SkipassDto> GetSkipassByID(int skipassId)
    {
        return mapper.Map<SkipassDto>(await context.Skipasses.FindAsync(skipassId));
    }

    public async Task<SkipassDto> AddNewSkipass(SkipassDto request)
    {
        var newSkipass = mapper.Map<Skipass>(request);
        var result = context.Skipasses.AddAsync(newSkipass);
        context.SaveChangesAsync();
        return mapper.Map<SkipassDto>(request);
    }

    public async Task<SkipassDto> UpdateSkipassInfo(SkipassDto updatedSkipass)
    {
        var skipassEntity = context.Skipasses.FindAsync((updatedSkipass.Id));
        var mappedEntity = mapper.Map<SkipassDto, Skipass>(updatedSkipass);
        var result = context.Skipasses.Update(mappedEntity);
        await context.SaveChangesAsync();
        return mapper.Map<Skipass, SkipassDto>(mappedEntity);
    }

    public async Task<SkipassDto> DeleteSkipass(int skipassId)
    {
        var skipassToDelete = await context.Skipasses.FindAsync(skipassId);
        var result = context.Skipasses.Remove(skipassToDelete);
        await context.SaveChangesAsync();
        return mapper.Map<SkipassDto>(result.Entity);
    }
}
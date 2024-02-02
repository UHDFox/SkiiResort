using Application.Exceptions;
using AutoMapper;
using Domain;
using Domain.Entities.Skipass;
using Domain.Entities.Visitor;
using Microsoft.EntityFrameworkCore;

namespace Application.Skipass;

internal class SkipassService : ISkipassService
{
    private readonly HotelContext context;
    private readonly IMapper mapper;

    public SkipassService(HotelContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<IReadOnlyCollection<GetSkipassModel>> GetListAsync(int offset, int limit)
    {
        return mapper.Map<IReadOnlyCollection<GetSkipassModel>>(await context.Skipasses.Skip(offset).Take(limit).ToListAsync());
    }

    public async Task<GetSkipassModel> GetByIdAsync(Guid skipassId)
    {
        var capy = await context.Skipasses
            .Include(record => record.Tariff)
            .Include(record => record.Visitor)
            .FirstOrDefaultAsync(x => x.Id == skipassId);
        return mapper.Map<GetSkipassModel>(capy);
    }

    public async Task<SkipassRecord> AddAsync(AddSkipassModel skipassModel)
    {
        var result = await context.Skipasses.AddAsync(mapper.Map<SkipassRecord>(skipassModel));
        await context.SaveChangesAsync();
        return result.Entity;
    }
    public async Task<bool> UpdateAsync(UpdateSkipassModel skipassModel)
    {
        var record = await GetByIdAsync(skipassModel.Id); 
        context.Skipasses.Update(mapper.Map<SkipassRecord>(record));
        return await context.SaveChangesAsync() > 0;
    }

    public async Task DeleteAsync(Guid id)
    {
        var record = context.Skipasses.FirstOrDefault(record => record.Id == id) ??
                     throw new NotFoundException("Skipass not found");
        context.Skipasses.Remove(record);
        await context.SaveChangesAsync();
    }
}


using Application.Exceptions;
using AutoMapper;
using Domain;
using Domain.Entities.Skipass;
using Microsoft.EntityFrameworkCore;

namespace Application.Skipass;

internal sealed class SkipassService : ISkipassService
{
    private readonly HotelContext context;
    private readonly IMapper mapper;
    private readonly ITariffService tariffService;
    private readonly IVisitorService visitorService;

    public SkipassService(HotelContext context, IMapper mapper, ITariffService tariffService, IVisitorService visitorService)
    {
        this.context = context;
        this.mapper = mapper;
        this.tariffService = tariffService;
        this.visitorService = visitorService;
    }

    public async Task<IReadOnlyCollection<GetSkipassModel>> GetListAsync(int offset, int limit)
    {
        return mapper.Map<IReadOnlyCollection<GetSkipassModel>>(await context.Skipasses.Skip(offset).Take(limit).ToListAsync());
    }

    public async Task<GetSkipassModel> GetByIdAsync(Guid skipassId)
    {
        var result = await context.Skipasses
            .AsNoTracking()
            .Include(record => record.Tariff)
            .Include(record => record.Visitor)
            .FirstOrDefaultAsync(x => x.Id == skipassId);
        return mapper.Map<GetSkipassModel>(result);
    }

    public async Task<SkipassRecord> AddAsync(AddSkipassModel skipassModel)
    {
        var result = await context.Skipasses.AddAsync(mapper.Map<SkipassRecord>(skipassModel));
        await context.SaveChangesAsync();
        return result.Entity;
    }
    public async Task<bool> UpdateAsync(UpdateSkipassModel skipassModel)
    {
        var record = await context.Skipasses
                         .AsNoTracking()
                         .FirstOrDefaultAsync(record => record.Id == skipassModel.Id)
                     ?? throw new NotFoundException();
        mapper.Map(skipassModel, record);
        context.Skipasses.Update(record);
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


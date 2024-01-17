using Application.Exceptions;
using Application.Tariff;
using Application.Visitor;
using AutoMapper;
using Domain;
using Domain.Entities.Skipass;
using Domain.Entities.Tariff;
using Microsoft.EntityFrameworkCore;

namespace Application.Skipass;

internal class SkipassService : ISkipassService
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
        return mapper.Map<IReadOnlyCollection<GetSkipassModel>>(await context.Skipasses.Skip(offset)
         .Take(limit).ToListAsync());
    }

    public async Task<SkipassRecord> GetByIdAsync(Guid skipassId)
    {
        return mapper.Map<SkipassRecord>(await context.Skipasses.FindAsync(skipassId)) ??
               throw new NotFoundException("Skipass not found");
    }

    public async Task<SkipassRecord> AddAsync(AddSkipassModel skipassModel)
    {
        var result = await context.Skipasses.AddAsync(mapper.Map<SkipassRecord>(skipassModel));
        await context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<bool> UpdateAsync(UpdateSkipassModel skipassModel)
    {
        var record = await GetByIdAsync(skipassModel.Id) ?? throw new NotFoundException();
        context.Skipasses.Update(mapper.Map(skipassModel, record));
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var record = context.Skipasses.FirstOrDefault(record => record.Id == id) ??
                     throw new NotFoundException("Skipass not found");
        context.Skipasses.Remove(record);
        return await context.SaveChangesAsync() > 0;
    }
}
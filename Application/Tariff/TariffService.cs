using Application.Exceptions;
using Application.Infrastructure.Automapper;
using AutoMapper;
using Domain;
using Domain.Entities.Tariff;
using Microsoft.EntityFrameworkCore;

namespace Application.Tariff;

internal class TariffService : ITariffService
{
    private readonly HotelContext context;
    private readonly IMapper mapper;

    public TariffService(HotelContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }
    public async Task<TariffRecord> GetByIdAsync(Guid skipassId)
    {

        return new TariffRecord("capy");
    }

    public async Task<IReadOnlyCollection<GetTariffModel>> GetListAsync(int? offset, int? limit)
    {
        /*return mapper.Map<IReadOnlyCollection<GetTariffModel>>(await context.Tariffs.Skip(offset).Take(limit)
            .ToListAsync());*/
        return mapper.Map<IReadOnlyCollection<GetTariffModel>>(await context.Tariffs.Skip((int)offset!).Take((int)limit!).ToListAsync());
    }
    
    public async Task<AddTariffModel> AddAsync(AddTariffModel tariffModel)
    {
        var result = await context.Tariffs.AddAsync(mapper.Map<TariffRecord>(tariffModel));
        await context.SaveChangesAsync();
        return mapper.Map<AddTariffModel>(result);
    }
}
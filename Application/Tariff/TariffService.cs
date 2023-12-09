using AutoMapper;
using Domain;
using Domain.Entities.Tariff;

namespace Application.Tariff;

internal class TariffService : ITariffService
{
    private readonly IHotelContext context;
    private readonly IMapper mapper;

    public TariffService(IHotelContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }
    
    public async Task<AddTariffModel> AddAsync(AddTariffModel tariffModel)
    {
        return mapper.Map<AddTariffModel>(await context.Tariffs.AddAsync(mapper.Map<TariffRecord>(tariffModel)));
        //return mapper.Map<AddSkipassModel>(await context.Skipasses.AddAsync(mapper.Map<SkipassRecord>(skipassModel)));
    }
}
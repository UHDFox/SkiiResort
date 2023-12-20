using Application.Exceptions;
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
    public async Task<GetTariffModel> GetByIdAsync(Guid skipassId)
    {
        return mapper.Map<GetTariffModel>(await context.Skipasses.FindAsync(skipassId)) ??
               throw new NotFoundException("Tariff not found");
    }

    public async Task<IReadOnlyCollection<GetTariffModel>> GetListAsync(int offset = 0, int limit = 150)
    {
        return mapper.Map <IReadOnlyCollection<GetTariffModel>>(await context.Tariffs.Take(limit).ToListAsync());
    }
    
    public async Task<AddTariffModel> AddAsync(AddTariffModel tariffModel)
    {
        /*await context.Tariffs.AddAsync(mapper.Map<TariffRecord>(tariffModel));
        return mapper.Map<AddTariffModel>(mapper.Map<TariffRecord>())*/
        //return mapper.Map<AddTariffModel>(await context.Tariffs.AddAsync(mapper.Map<TariffRecord>(tariffModel)));
        return mapper.Map<AddTariffModel>(await context.Tariffs.AddAsync(mapper.Map<TariffRecord>(tariffModel)));
    }
}
using Application.Exceptions;
using AutoMapper;
using Domain;
using Domain.Entities.Tariff;
using Microsoft.EntityFrameworkCore;

namespace Application.Tariff;

internal sealed class TariffService : ITariffService
{
    private readonly HotelContext context;
    private readonly IMapper mapper;

    public TariffService(HotelContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<TariffRecord> GetByIdAsync(Guid id)
    {
        var tariff = await context.Tariffs.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id) ??
                     throw new NotFoundException();
        return tariff;
    }

    public async Task<IReadOnlyCollection<GetTariffModel>> GetListAsync(int offset, int limit)
    {
        return mapper.Map<IReadOnlyCollection<GetTariffModel>>(await context.Tariffs.Skip(offset)
            .Take(limit).ToListAsync());
    }

    public async Task<TariffRecord> AddAsync(AddTariffModel tariffModel)
    {
        var result = await context.Tariffs.AddAsync(mapper.Map<TariffRecord>(tariffModel));
        await context.SaveChangesAsync();
        return mapper.Map<TariffRecord>(result.Entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var tariff = await GetByIdAsync(id);
        context.Tariffs.Remove(tariff);
        await context.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(UpdateTariffModel tariffModel)
    {
        var tariff = await GetByIdAsync(tariffModel.Id);
        context.Tariffs.Update(mapper.Map(tariffModel, tariff));
        return await context.SaveChangesAsync() > 0;
    }
}
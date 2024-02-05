using Application.Exceptions;
using AutoMapper;
using Domain.Entities.Tariff;
using Repository.Tariff;

namespace Application.Tariff;

internal sealed class TariffService : ITariffService
{
    private readonly IMapper mapper;
    private readonly ITariffRepository repository;

    public TariffService(IMapper mapper, ITariffRepository repository)
    {
        this.mapper = mapper;
        this.repository = repository;
    }

    public async Task<GetTariffModel> GetByIdAsync(Guid id)
    {
        var tariff = await repository.GetByIdAsync(id) ?? throw new NotFoundException();
        return mapper.Map<GetTariffModel>(tariff);
    }

    public async Task<IReadOnlyCollection<GetTariffModel>> GetListAsync(int offset, int limit)
    {
        return mapper.Map<IReadOnlyCollection<GetTariffModel>>(await repository.GetListAsync(offset, limit));
    }

    public async Task<Guid> AddAsync(AddTariffModel tariffModel)
    {
        var result = await repository.AddAsync(mapper.Map<TariffRecord>(tariffModel));
        return result;
    }

    public async Task DeleteAsync(Guid id)
    {
        await GetByIdAsync(id);
        await repository.DeleteAsync(id);
    }

    public async Task<bool> UpdateAsync(UpdateTariffModel tariffModel)
    {
        await GetByIdAsync(tariffModel.Id);
        return await repository.UpdateAsync(mapper.Map<TariffRecord>(tariffModel));
    }
}
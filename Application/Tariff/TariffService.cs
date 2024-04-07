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

    public async Task<bool> DeleteAsync(Guid id)
    {
        await GetByIdAsync(id);
        return await repository.DeleteAsync(id);
    }

    public async Task<UpdateTariffModel> UpdateAsync(UpdateTariffModel tariffModel)
    {
        var entity = await repository.GetByIdAsync(tariffModel.Id)
                     ?? throw new NotFoundException("tariff entity not found");
        
        mapper.Map(tariffModel, entity);
        
        repository.UpdateAsync(entity);
        await repository.SaveChangesAsync();
        
        return tariffModel;
    }
}
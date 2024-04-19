using AutoMapper;
using SkiiResort.Application.Exceptions;
using SkiiResort.Domain.Entities.Tariff;
using SkiiResort.Repository.Tariff;

namespace SkiiResort.Application.Tariff;

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
        var totalAmount = await repository.GetTotalAmountAsync();

        if (totalAmount < offset)
        {
            throw new PaginationQueryException("offset exceeds total amount of records");
        }

        if (totalAmount < offset + limit)
        {
            throw new PaginationQueryException("queried page exceeds total amount of records");
        }

        return mapper.Map<IReadOnlyCollection<GetTariffModel>>(await repository.GetAllAsync(offset, limit));
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

        repository.Update(entity);
        await repository.SaveChangesAsync();

        return tariffModel;
    }
}

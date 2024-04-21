using AutoMapper;
using SkiiResort.Application.Exceptions;
using SkiiResort.Application.Tariffication.Models;
using SkiiResort.Domain.Entities.Tariffication;
using SkiiResort.Repository.Tariffication;

namespace SkiiResort.Application.Tariffication;

public sealed class TarifficationService : ITarifficationService
{
    private readonly ITarifficationRepository repository;
    private readonly IMapper mapper;

    public TarifficationService(ITarifficationRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<IReadOnlyCollection<GetTarifficationModel>> GetAllAsync(int offset, int limit)
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

        return mapper.Map<IReadOnlyCollection<GetTarifficationModel>>(await repository.GetAllAsync(offset, limit));
    }

    public async Task<GetTarifficationModel> GetByIdAsync(Guid id)
    {
        var result = await repository.GetByIdAsync(id) ?? throw new NotFoundException();
        return mapper.Map<GetTarifficationModel>(result);
    }

    public async Task<Guid> AddAsync(AddTarifficationModel model)
    {
        return await repository.AddAsync(mapper.Map<TarifficationRecord>(model));
    }

    public async Task<UpdateTarifficationModel> UpdateAsync(UpdateTarifficationModel model)
    {
        var entity = await repository.GetByIdAsync(model.Id)
            ?? throw new NotFoundException("tariffication record not found");

        mapper.Map(model, entity);

        repository.Update(entity);
        await repository.SaveChangesAsync();

        return model;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        await GetByIdAsync(id); //to check if such an entity exists
        return await repository.DeleteAsync(id);
    }
}

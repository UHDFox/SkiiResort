using Application.Exceptions;
using Application.Tariffication.Models;
using AutoMapper;
using Domain.Entities.Tariffication;
using Repository.Tariffication;

namespace Application.Tariffication;

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
        return mapper.Map<IReadOnlyCollection<GetTarifficationModel>>(await repository.GetListAsync(offset, limit));
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

    public async Task<bool> UpdateAsync(UpdateTarifficationModel model)
    {
        await repository.GetByIdAsync(model.Id);
        return await repository.UpdateAsync(mapper.Map<TarifficationRecord>(model));
    }

    public async Task DeleteAsync(Guid id)
    {
        await repository.GetByIdAsync(id); //to check if such an entity exists
        await repository.DeleteAsync(id);
    }
}
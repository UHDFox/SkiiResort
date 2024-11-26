using Application;
using AutoMapper;
using SkiiResort.Application.Exceptions;
using SkiiResort.Domain.Entities;
using SkiiResort.Repository;

namespace SkiiResort.Application;

public abstract class Service<TModel, TRecord> : IService<TModel>
    where TModel : ServiceModel
    where TRecord : DataObject
{
    private readonly IMapper mapper;
    private readonly IRepository<TRecord> repository;

    public Service(IRepository<TRecord> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }


    public async Task<IReadOnlyCollection<TModel>> GetListAsync(int offset, int limit)
    {
        var totalAmount = await repository.GetTotalAmountAsync();

        return mapper.Map<IReadOnlyCollection<TModel>>(await repository.GetAllAsync(offset, limit));
    }

    public async Task<TModel> GetByIdAsync(Guid id)
    {
        var result = await repository.GetByIdAsync(id) ?? throw new Exception();
        return mapper.Map<TModel>(result);
    }

    public async Task<Guid> AddAsync(TModel userModel)
    {
        var entity = mapper.Map<TModel>(userModel);

        var result = await repository.AddAsync(mapper.Map<TRecord>(entity));

        return result;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        await GetByIdAsync(id);
        return await repository.DeleteAsync(id);
    }

    public async Task<TModel> UpdateAsync(TModel userModel)
    {
        var entity = await repository.GetByIdAsync(userModel.Id)
                     ?? throw new NotFoundException("Entity not found");

        mapper.Map(userModel, entity);

        repository.UpdateAsync(entity);
        await repository.SaveChangesAsync();

        return userModel;
    }

    public async Task<int> SaveChangesAsync() => await repository.SaveChangesAsync();
}

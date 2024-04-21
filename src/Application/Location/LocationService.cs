using AutoMapper;
using SkiiResort.Application.Exceptions;
using SkiiResort.Application.Location.Models;
using SkiiResort.Domain.Entities.Location;
using SkiiResort.Repository.Location;

namespace SkiiResort.Application.Location;

internal sealed class LocationService : ILocationService
{
    private readonly IMapper mapper;
    private readonly ILocationRepository repository;

    public LocationService(ILocationRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<IReadOnlyCollection<GetLocationModel>> GetAllAsync(int offset, int limit)
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

        return mapper.Map<IReadOnlyCollection<GetLocationModel>>(await repository.GetAllAsync(offset, limit));
    }

    public async Task<GetLocationModel> GetByIdAsync(Guid id)
    {
        var result = await repository.GetByIdAsync(id) ?? throw new NotFoundException();
        return mapper.Map<GetLocationModel>(result);
    }

    public async Task<Guid> AddAsync(AddLocationModel model)
    {
        var id = await repository.AddAsync(mapper.Map<LocationRecord>(model));
        return id;
    }

    public async Task<UpdateLocationModel> UpdateAsync(UpdateLocationModel model)
    {
        var entity = await repository.GetByIdAsync(model.Id)
                     ?? throw new NotFoundException("location entity not found");

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

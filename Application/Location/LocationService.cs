using Application.Exceptions;
using Application.Location.Models;
using AutoMapper;
using Domain.Entities.Location;
using Repository.Location;

namespace Application.Location;

internal sealed class LocationService : ILocationService
{
    private readonly ILocationRepository repository;
    private readonly IMapper mapper;
    
    public LocationService(ILocationRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }
    
    public async Task<IReadOnlyCollection<GetLocationModel>> GetAllAsync(int offset, int limit)
    {
        return mapper.Map<IReadOnlyCollection<GetLocationModel>>(await repository.GetListAsync(offset, limit));
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
        
        repository.UpdateAsync(entity);
        
        await repository.SaveChangesAsync();
        
        return model;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        await repository.GetByIdAsync(id); //to check if such an entity exists
        return await repository.DeleteAsync(id);
    }
}
using AutoMapper;
using SkiiResort.Domain.Entities.Location;
using SkiiResort.Repository.Location;

namespace SkiiResort.Application.Location;

internal sealed class LocationService : Service<LocationModel, LocationRecord>, ILocationService
{
    private readonly IMapper mapper;
    private readonly ILocationRepository repository;

    public LocationService(ILocationRepository repository, IMapper mapper)
    : base(repository, mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }
}

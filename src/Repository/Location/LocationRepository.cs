using Microsoft.EntityFrameworkCore;
using SkiiResort.Domain;
using SkiiResort.Domain.Entities.Location;

namespace SkiiResort.Repository.Location;

internal sealed class LocationRepository : Repository<LocationRecord>, ILocationRepository
{
    public LocationRepository(SkiiResortContext context) : base(context)
    {
    }
}

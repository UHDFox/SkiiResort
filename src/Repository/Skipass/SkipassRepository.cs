using Microsoft.EntityFrameworkCore;
using SkiiResort.Domain;
using SkiiResort.Domain.Entities.Skipass;

namespace SkiiResort.Repository.Skipass;

internal sealed class SkipassRepository : Repository<SkipassRecord>, ISkipassRepository
{
    public SkipassRepository(SkiiResortContext context) : base(context)
    {
    }
}

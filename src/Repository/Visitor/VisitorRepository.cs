using Microsoft.EntityFrameworkCore;
using SkiiResort.Domain;
using SkiiResort.Domain.Entities.Visitor;

namespace SkiiResort.Repository.Visitor;

internal sealed class VisitorRepository : Repository<VisitorRecord>, IVisitorRepository
{
    public VisitorRepository(SkiiResortContext context) : base(context)
    {
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SkiiResort.Domain;
using SkiiResort.Domain.Entities.VisitorsAction;

namespace SkiiResort.Repository.VisitorActions;

internal sealed class VisitorActionsRepository : Repository<VisitorActionsRecord>,IVisitorActionsRepository
{
    private readonly SkiiResortContext context;

    public VisitorActionsRepository(SkiiResortContext context) : base(context)
    {
        this.context = context;
    }
    public async Task<IDbContextTransaction> BeginTransaction() => await context.Database.BeginTransactionAsync();
}

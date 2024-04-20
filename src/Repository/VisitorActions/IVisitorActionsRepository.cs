using Microsoft.EntityFrameworkCore.Storage;
using SkiiResort.Domain.Entities.VisitorsAction;

namespace SkiiResort.Repository.VisitorActions;

public interface IVisitorActionsRepository : IRepository<VisitorActionsRecord>
{
    Task<IDbContextTransaction> BeginTransaction();
}

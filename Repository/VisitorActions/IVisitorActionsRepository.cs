using Domain.Entities.VisitorsAction;
using Microsoft.EntityFrameworkCore.Storage;

namespace Repository.VisitorActions;

public interface IVisitorActionsRepository : IRepository<VisitorActionsRecord>
{
    Task<IDbContextTransaction> BeginTransaction();
}
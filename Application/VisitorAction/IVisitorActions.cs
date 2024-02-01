using Domain.Entities.Tariff;
using Domain.Entities.VisitorsAction;

namespace Application.VisitorAction;

public interface IVisitorActions
{
    Task<IReadOnlyCollection<GetVisitorActionsModel>> GetAllAsync(int offset, int limit);
    Task<VisitorActionsRecord> GetByIdAsync(Guid id);
    Task<VisitorActionsRecord> AddAsync(AddVisitorActionsModel model);
    Task<bool> UpdateAsync(UpdateVisitorActionsModel model);
    Task DeleteAsync(Guid id);

}
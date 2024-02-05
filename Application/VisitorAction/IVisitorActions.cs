namespace Application.VisitorAction;

public interface IVisitorActions
{
    Task<IReadOnlyCollection<GetVisitorActionsModel>> GetAllAsync(int offset, int limit);

    Task<GetVisitorActionsModel> GetByIdAsync(Guid id);

    Task<Guid> AddAsync(AddVisitorActionsModel model);

    Task<bool> UpdateAsync(UpdateVisitorActionsModel model);

    Task DeleteAsync(Guid id);
}
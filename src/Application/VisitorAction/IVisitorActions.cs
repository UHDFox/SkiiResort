namespace SkiiResort.Application.VisitorAction;

public interface IVisitorActions
{
    Task<IReadOnlyCollection<GetVisitorActionsModel>> GetAllAsync(int offset, int limit);

    Task<GetVisitorActionsModel> GetByIdAsync(Guid id);

    Task<Guid> AddAsync(AddVisitorActionsModel model);

    Task<Guid> TapSkipass(AddVisitorActionsModel model);

    Task<Guid> DepositSkipassBalance(AddVisitorActionsModel model);

    Task<UpdateVisitorActionsModel> UpdateAsync(UpdateVisitorActionsModel model);

    Task<bool> DeleteAsync(Guid id);
}

using Application;

namespace SkiiResort.Application.VisitorAction;

public interface IVisitorActionsService : IService<VisitorActionsModel>
{
    Task<Guid> TapSkipass(AddVisitorActionsModel model);

    Task<Guid> DepositSkipassBalance(AddVisitorActionsModel model);
}

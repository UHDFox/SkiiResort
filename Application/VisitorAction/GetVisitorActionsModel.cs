using Domain.Entities.Location;
using Domain.Entities.Skipass;
using Domain.Enums;

namespace Application.VisitorAction;

public sealed class GetVisitorActionsModel
{
    public GetVisitorActionsModel(Guid id, Guid skipassId,Guid locationId, DateTimeOffset time, int balanceChange, OperationType transactionType)
    {
        Id = id;
        SkipassId = skipassId;
        LocationId = locationId;
        Time = time;
        BalanceChange = balanceChange;
        TransactionType = transactionType;
    }

    public Guid Id { get; set; }

    public Guid SkipassId { get; set; }

    public SkipassRecord? Skipass { get; set; }
    
    public Guid LocationId { get; set; }

    public LocationRecord? Location { get; set; }

    public DateTimeOffset Time { get; set; }

    public int BalanceChange { get; set; }
    
    public OperationType TransactionType { get; set; }
}
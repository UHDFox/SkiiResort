using Domain.Enums;

namespace Application.VisitorAction;

public sealed class AddVisitorActionsModel
{
    public AddVisitorActionsModel(Guid skipassId, Guid locationId)
    {
        SkipassId = skipassId;
        LocationId = locationId;
    }

    public Guid SkipassId { get; set; }
    
    public Guid LocationId { get; set; }

    public DateTimeOffset? Time { get; set; } = DateTimeOffset.UtcNow;

    public double? BalanceChange { get; set; }

    public OperationType? TransactionType { get; set; }
}
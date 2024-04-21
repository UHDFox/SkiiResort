using SkiiResort.Domain.Enums;

namespace SkiiResort.Application.VisitorAction;

public sealed class UpdateVisitorActionsModel
{
    public UpdateVisitorActionsModel(Guid id, Guid skipassId, Guid locationId)
    {
        Id = id;
        SkipassId = skipassId;
        LocationId = locationId;
    }

    public Guid Id { get; set; }

    public Guid SkipassId { get; set; }

    public Guid LocationId { get; set; }

    public DateTimeOffset? Time { get; set; } = DateTimeOffset.UtcNow;

    public double? BalanceChange { get; set; }

    public OperationType? TransactionType { get; set; }
}

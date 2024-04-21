using SkiiResort.Domain.Enums;

namespace SkiiResort.Web.Contracts.VisitorActions;

public sealed class VisitorActionsResponse
{
    public VisitorActionsResponse(Guid id, Guid skipassId, Guid locationId, DateTimeOffset time, double balanceChange, OperationType transactionType)
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

    public Guid LocationId { get; set; }

    public DateTimeOffset Time { get; set; }

    public double BalanceChange { get; set; }

    public OperationType TransactionType { get; set; }
}

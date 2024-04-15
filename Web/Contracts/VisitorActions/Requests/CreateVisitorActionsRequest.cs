using SkiiResort.Domain.Enums;

namespace SkiiResort.Web.Contracts.VisitorActions.Requests;

public sealed class CreateVisitorActionsRequest
{
    public CreateVisitorActionsRequest(Guid skipassId, Guid locationId, DateTimeOffset time, double balanceChange, OperationType transactionType)
    {
        SkipassId = skipassId;
        LocationId = locationId;
        Time = time;
        BalanceChange = balanceChange;
        TransactionType = transactionType;
    }

    public Guid SkipassId { get; set; }

    public Guid LocationId { get; set; }

    public DateTimeOffset Time { get; set; }

    public double BalanceChange { get; set; }

    public OperationType TransactionType { get; set; }
}

using SkiiResort.Domain.Entities.Location;
using SkiiResort.Domain.Entities.Skipass;
using SkiiResort.Domain.Enums;

namespace SkiiResort.Domain.Entities.VisitorsAction;

public sealed class VisitorActionsRecord : DataObject
{
    public VisitorActionsRecord(Guid skipassId, Guid locationId, DateTimeOffset time, double balanceChange, OperationType transactionType)
    {
        SkipassId = skipassId;
        LocationId = locationId;
        Time = time;
        BalanceChange = balanceChange;
        TransactionType = transactionType;
    }

    public Guid SkipassId { get; set; }

    public SkipassRecord? Skipass { get; set; }

    public Guid LocationId { get; set; }

    public LocationRecord? Location { get; set; }

    public DateTimeOffset Time { get; set; }

    public double BalanceChange { get; set; }

    public OperationType TransactionType { get; set; }
}

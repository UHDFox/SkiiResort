using Domain.Entities.Location;
using Domain.Entities.Skipass;

namespace Domain.Entities.VisitorsAction;

public sealed class VisitorActionsRecord
{
    public VisitorActionsRecord(Guid skipassId, Guid locationId, DateTimeOffset time, int balanceChange)
    {
        SkipassId = skipassId;
        LocationId = locationId;
        Time = time;
        BalanceChange = balanceChange;
    }


    public Guid Id { get; set; }

    public Guid SkipassId { get; set; }

    public SkipassRecord? Skipass { get; set; }

    public Guid LocationId { get; set; }

    public LocationRecord? Location { get; set; }

    public DateTimeOffset Time { get; set; }

    public int BalanceChange { get; set; }
}
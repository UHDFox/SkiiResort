using Domain.Entities.Skipass;
using Domain.Enums;

namespace Domain.Entities.VisitorsAction;

public sealed class VisitorActionsRecord
{
    public VisitorActionsRecord(Guid skipassId, Place place, DateTimeOffset time, int balanceChange)
    {
        SkipassId = skipassId;
        Place = place;
        Time = time;
        BalanceChange = balanceChange;
    }


    public Guid Id { get; set; }

    public Guid SkipassId { get; set; }

    public SkipassRecord? Skipass { get; set; }

    public Place Place { get; set; }

    public DateTimeOffset Time { get; set; }

    public int BalanceChange { get; set; }
}
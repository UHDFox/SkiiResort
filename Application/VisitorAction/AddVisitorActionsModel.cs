using Domain.Enums;

namespace Application.VisitorAction;

public sealed class AddVisitorActionsModel
{
    public AddVisitorActionsModel(Guid skipassId, Place place, DateTimeOffset time, int balanceChange)
    {
        SkipassId = skipassId;
        Place = place;
        Time = time;
        BalanceChange = balanceChange;
    }

    public Guid SkipassId { get; set; }

    public Place Place { get; set; }

    public DateTimeOffset Time { get; set; }

    public int BalanceChange { get; set; }
}
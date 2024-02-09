using Domain.Enums;

namespace Application.VisitorAction;

public sealed class UpdateVisitorActionsModel
{
    public UpdateVisitorActionsModel(Guid id, Guid skipassId, Place place, DateTimeOffset time, int balanceChange)
    {
        Id = id;
        SkipassId = skipassId;
        Place = place;
        Time = time;
        BalanceChange = balanceChange;
    }

    public Guid Id { get; set; }

    public Guid SkipassId { get; set; }

    public Place Place { get; set; }

    public DateTimeOffset Time { get; set; }

    public int BalanceChange { get; set; }
}
using Domain.Enums;

namespace Web.Contracts.VisitorActions.Requests;

public sealed class UpdateVisitorActionsRequest
{
    public UpdateVisitorActionsRequest(Guid id, Guid skipassId, Place place, DateTime time, int balanceChange)
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

    public DateTime Time { get; set; }

    public int BalanceChange { get; set; }
}
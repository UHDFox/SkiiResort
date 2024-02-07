using Domain.Enums;

namespace Web.Contracts.VisitorActions.Requests;

public sealed class CreateVisitorActionsRequest
{
    public CreateVisitorActionsRequest(Guid skipassId, Place place, DateTime time, int balanceChange)
    {
        SkipassId = skipassId;
        Place = place;
        Time = time;
        BalanceChange = balanceChange;
    }

    public Guid SkipassId { get; set; }

    public Place Place { get; set; }

    public DateTime Time { get; set; }

    public int BalanceChange { get; set; }
}
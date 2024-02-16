namespace Web.Contracts.VisitorActions.Requests;

public sealed class CreateVisitorActionsRequest
{
    public CreateVisitorActionsRequest(Guid skipassId, Guid locationId, DateTimeOffset time, int balanceChange)
    {
        SkipassId = skipassId;
        LocationId = locationId;
        Time = time;
        BalanceChange = balanceChange;
    }

    public Guid SkipassId { get; set; }
    
    public Guid LocationId { get; set; }

    public DateTimeOffset Time { get; set; }

    public int BalanceChange { get; set; }
}
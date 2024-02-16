namespace Web.Contracts.VisitorActions.Requests;

public sealed class UpdateVisitorActionsRequest
{
    public UpdateVisitorActionsRequest(Guid id, Guid skipassId, Guid locationId, DateTimeOffset time, int balanceChange)
    {
        Id = id;
        SkipassId = skipassId;
        LocationId = locationId;
        Time = time;
        BalanceChange = balanceChange;
    }

    public Guid Id { get; set; }

    public Guid SkipassId { get; set; }
    
    public Guid LocationId { get; set; }

    public DateTimeOffset Time { get; set; }

    public int BalanceChange { get; set; }
}
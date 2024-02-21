namespace Web.Contracts.Skipass.Requests;

public sealed class TapSkipassRequest
{
    public TapSkipassRequest(Guid skipassId, Guid locationId)
    {
        SkipassId = skipassId;
        LocationId = locationId;
    }

    public Guid SkipassId { get; set; }
    
    public Guid LocationId { get; set; }
}
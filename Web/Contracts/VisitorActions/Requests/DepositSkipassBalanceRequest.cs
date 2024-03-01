using Domain.Enums;

namespace Web.Contracts.VisitorActions.Requests;

public sealed class DepositSkipassBalanceRequest
{
    public DepositSkipassBalanceRequest(Guid skipassId, Guid locationId, double balanceChange)
    {
        SkipassId = skipassId;
        LocationId = locationId;
        BalanceChange = balanceChange;
    }
    
    public Guid SkipassId { get; set; }
    
    public Guid LocationId { get; set; } 

    public double BalanceChange { get; set; }
}
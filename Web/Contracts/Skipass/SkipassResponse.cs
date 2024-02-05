namespace Web.Contracts.Skipass;

public sealed class SkipassResponse
{
    public SkipassResponse(Guid id, int balance, Guid tariffId,  Guid visitorId, bool status, bool isVip)
    {
        Id = id;
        Balance = balance;
        TariffId = tariffId;
        VisitorId = visitorId;
        Status = status;
        IsVip = isVip;
    }
    
    
    public Guid Id { get; set; }
    
    public int Balance { get; set; }
    
    public Guid TariffId { get; set; }
    
    public Guid VisitorId { get; set; }
    
    public bool Status { get; set; }
    
    public bool IsVip { get; set; }
}
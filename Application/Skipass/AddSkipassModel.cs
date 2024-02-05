namespace Application.Skipass;

public sealed class AddSkipassModel
{
    public AddSkipassModel(int balance, Guid tariffId, Guid visitorId, bool status, bool isVip)
    {
        Balance = balance;
        TariffId = tariffId;
        VisitorId = visitorId;
        Status = status;
        IsVip = isVip;
    }
    public int Balance { get; set; }

    public Guid TariffId { get; set; }

    public Guid VisitorId { get; set; }

    public bool Status { get; set; }
    
    public bool IsVip { get; set; }
}
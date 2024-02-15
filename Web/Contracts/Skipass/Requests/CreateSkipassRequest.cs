namespace Web.Contracts.Skipass.Requests;

public sealed class CreateSkipassRequest
{
    public CreateSkipassRequest(int balance, Guid tariffId, Guid visitorId, bool status)
    {
        Balance = balance;
        TariffId = tariffId;
        VisitorId = visitorId;
        Status = status;
    }

    public int Balance { get; set; }

    public Guid TariffId { get; set; }

    public Guid VisitorId { get; set; }

    public bool Status { get; set; }
}
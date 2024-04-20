namespace SkiiResort.Application.Skipass;

public sealed class UpdateSkipassModel
{
    public UpdateSkipassModel(Guid id, double balance, Guid tariffId, Guid visitorId, bool status)
    {
        Id = id;
        Balance = balance;
        TariffId = tariffId;
        VisitorId = visitorId;
        Status = status;
    }

    public Guid Id { get; set; }

    public double Balance { get; set; }

    public Guid TariffId { get; set; }

    public Guid VisitorId { get; set; }

    public bool Status { get; set; }
}

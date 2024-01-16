using Domain.Entities.Tariff;
using Domain.Entities.Visitor;

namespace Application.Skipass;

public sealed class AddSkipassModel
{
    public AddSkipassModel(int balance, Guid tariffId, Guid visitorId, bool status)
    {
        Balance = balance;
        TariffId = tariffId;
        VisitorId = visitorId;
        Status = status;
    }
    //public Guid Id { get; set; }
    public int Balance { get; set; }
    public Guid TariffId { get; set; }
    //public TariffRecord? TariffRecord { get; set; }
    public Guid VisitorId { get; set; }
    //public VisitorRecord? VisitorRecord { get; set; }
    public bool Status { get; set; }
}
using Domain.Entities.Tariff;
using Domain.Entities.Visitor;

namespace Domain.Entities.Skipass;

public sealed class SkipassRecord
{
    public SkipassRecord(Guid id, int balance, Guid tariffId, Guid visitorId, bool status)
    {
        Id = id;
        Balance = balance;
        TariffId = tariffId;
        VisitorId = visitorId;
        Status = status;
    }

    public Guid Id { get; set; }
    public int Balance { get; set; }
    public Guid TariffId { get; set; }
    public TariffRecord? TariffRecord { get; set; }
    public Guid VisitorId { get; set; }
    public VisitorRecord? VisitorRecord { get; set; }
    public bool Status { get; set; }
}
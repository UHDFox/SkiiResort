using SkiiResort.Domain.Entities.Tariff;
using SkiiResort.Domain.Entities.Visitor;
using SkiiResort.Domain.Entities.VisitorsAction;

namespace SkiiResort.Domain.Entities.Skipass;

public sealed class SkipassRecord
{
    public SkipassRecord(double balance, Guid tariffId, Guid visitorId, bool status)
    {
        Balance = balance;
        TariffId = tariffId;
        VisitorId = visitorId;
        Status = status;
    }

    public Guid Id { get; set; }

    public double Balance { get; set; }

    public Guid TariffId { get; set; }

    public TariffRecord? Tariff { get; set; }

    public Guid VisitorId { get; set; }

    public VisitorRecord? Visitor { get; set; }

    public bool Status { get; set; }

    public IReadOnlyList<VisitorActionsRecord> VisitorActions { get; set; } = new List<VisitorActionsRecord>();
}

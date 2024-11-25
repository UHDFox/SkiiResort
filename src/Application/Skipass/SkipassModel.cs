using Application;
using SkiiResort.Domain.Entities.Tariff;
using SkiiResort.Domain.Entities.Visitor;

namespace SkiiResort.Application.Skipass;

public sealed class SkipassModel : ServiceModel
{
    public SkipassModel() : base(Guid.Empty)
    {
    }

    public SkipassModel(Guid id, double balance, Guid tariffId, Guid visitorId, bool status)
    : base(id)
    {
        Id = id;
        Balance = balance;
        TariffId = tariffId;
        VisitorId = visitorId;
        Status = status;
    }

    public double Balance { get; set; }

    public Guid TariffId { get; set; }

    public TariffRecord? Tariff { get; set; }

    public Guid VisitorId { get; set; }

    public VisitorRecord? Visitor { get; set; }

    public bool Status { get; set; }
}

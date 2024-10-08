using SkiiResort.Domain.Entities.Tariff;
using SkiiResort.Domain.Entities.Visitor;

namespace SkiiResort.Application.Skipass;

public sealed class GetSkipassModel
{
    public GetSkipassModel(Guid id, double balance, Guid tariffId, Guid visitorId, bool status)
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

    public TariffRecord? Tariff { get; set; }

    public Guid VisitorId { get; set; }

    public VisitorRecord? Visitor { get; set; }

    public bool Status { get; set; }
}

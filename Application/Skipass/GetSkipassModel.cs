using Domain.Entities.Tariff;
using Domain.Entities.Visitor;

namespace Application.Skipass;

public sealed class GetSkipassModel
{
    public GetSkipassModel(Guid id, int balance, Guid tariffId, Guid visitorId, bool status)
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

    public TariffRecord? Tariff { get; set; }

    public Guid VisitorId { get; set; }

    public VisitorRecord? Visitor { get; set; }

    public bool Status { get; set; }

    public bool IsVip { get; set; }
}
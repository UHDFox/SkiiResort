using Domain.Entities.Tariff;
using Domain.Entities.Visitor;

namespace Application.Skipass;

public sealed class GetSkipassModel
{
    public Guid Id { get; set; }
    public int Balance { get; set; }
    public Guid TariffId { get; set; }
    public TariffRecord? TariffRecord { get; set; }
    public Guid VisitorId { get; set; }
    public VisitorRecord? VisitorRecord { get; set; }
    public bool Status { get; set; }
}
using Domain.Entities.Tariff;
using Domain.Entities.Visitor;

namespace Application.Skipass;

public sealed class AddSkipassModel
{
    public Guid Id { get; set; }
    public int Balance { get; set; }
    public int TariffId { get; set; }
    public TariffRecord TariffRecord { get; set; }
    public int VisitorId { get; set; }
    public VisitorRecord VisitorRecord { get; set; }
    public bool Status { get; set; }
}
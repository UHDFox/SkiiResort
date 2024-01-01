using Application.Tariff;
using Application.Visitor;

namespace Application.Skipass;

public sealed class SkipassDto
{
    public Guid Id { get; set; }
    public int Balance { get; set; }
    public Guid TariffId { get; set; }
    public TariffDto? Tariff { get; set; }
    public Guid VisitorId { get; set; }
    public VisitorDto? Visitor { get; set; }
    public bool Status { get; set; }
}
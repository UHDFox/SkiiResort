using Application.Tariff;
using Application.Visitor;

namespace Application.Skipass;

public class SkipassDto
{
    public Guid Id { get; set; }
    public int Balance { get; set; }
    public int TariffId { get; set; }
    public TariffDto Tariff { get; set; }
    public int VisitorId { get; set; }
    public VisitorDto Visitor { get; set; }
    public bool Status { get; set; }
}
using Application.Entities.Visitor;
using Application.Entities.Tariff;

namespace Application.Entities;

public class SkipassDto
{
    public int Id {  get; set; }
    public int Balance { get; set; }
    public int TariffId { get; set; }
    public TariffDto Tariff { get; set; }
    public int VisitorId { get; set; }
    public VisitorDto Visitor { get; set; }
    public bool Status { get; set; }
}
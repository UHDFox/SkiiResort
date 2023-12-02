namespace Domain.Entities.Skipass;

public sealed class SkipassRecord
{
    public int Id { get; set; }
    public int Balance { get; set; }
    public int TariffId { get; set; }
    public Tariff.TariffRecord TariffRecord { get; set; }
    public int VisitorId { get; set; }
    public Visitor.VisitorRecord VisitorRecord { get; set; }
    public bool Status { get; set; }
}
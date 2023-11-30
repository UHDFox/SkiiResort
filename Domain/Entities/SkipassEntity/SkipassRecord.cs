namespace Domain.Entities.SkipassEntity;

public sealed class SkipassRecord
{
    public int Id { get; set; }
    public int Balance { get; set; }
    public int TariffId { get; set; }
    public Tariff.Tariff Tariff { get; set; }
    public int VisitorId { get; set; }
    public Visitor.Visitor Visitor { get; set; }
    public bool Status { get; set; }
}
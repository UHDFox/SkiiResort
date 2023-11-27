using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public sealed class Skipass
{
    public int Id {  get; set; }
    public int Balance { get; set; }
    public int TariffId { get; set; }
    public Tariff Tariff { get; set; }
    public int VisitorId { get; set; }
    public Visitor Visitor { get; set; }
    public bool Status { get; set; }
}
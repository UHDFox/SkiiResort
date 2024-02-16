using Domain.Entities.Skipass;
using Domain.Entities.Tariffication;

namespace Domain.Entities.Tariff;

public sealed class TariffRecord
{
    public TariffRecord(string name, double priceModifier, bool isVip)
    {
        Name = name;
        PriceModifier = priceModifier;
        IsVip = isVip;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }
    
    public double PriceModifier { get; set; }
    
    public bool IsVip { get; set; }

    public IReadOnlyList<SkipassRecord> Skipasses { get; set; } = new List<SkipassRecord>();
    
    public IReadOnlyList<TarifficationRecord> Tariffications { get; set; } = new List<TarifficationRecord>();
}
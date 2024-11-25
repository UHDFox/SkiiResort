using SkiiResort.Domain.Entities.Skipass;
using SkiiResort.Domain.Entities.Tariffication;

namespace SkiiResort.Domain.Entities.Tariff;

public sealed class TariffRecord : DataObject
{
    public TariffRecord(string name, double priceModifier, bool isVip)
    {
        Name = name;
        PriceModifier = priceModifier;
        IsVip = isVip;
    }
    public string Name { get; set; }

    public double PriceModifier { get; set; }

    public bool IsVip { get; set; }

    public IReadOnlyList<SkipassRecord> Skipasses { get; set; } = new List<SkipassRecord>();

    public IReadOnlyList<TarifficationRecord> Tariffications { get; set; } = new List<TarifficationRecord>();
}

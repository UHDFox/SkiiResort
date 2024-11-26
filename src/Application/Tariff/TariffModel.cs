using Application;
using SkiiResort.Domain.Entities.Skipass;
using SkiiResort.Domain.Entities.Tariffication;

namespace SkiiResort.Application.Tariff;

public class TariffModel : ServiceModel
{
    public TariffModel() : base(Guid.Empty)
    {
    }

    public TariffModel(Guid id, string name, double priceModifier, bool isVip)
        : base(id)
    {
        Name = name;
        PriceModifier = priceModifier;
        IsVip = isVip;
    }

    public string Name { get; set; } = "";

    public double PriceModifier { get; set; }

    public bool IsVip { get; set; }

    public IReadOnlyList<SkipassRecord> Skipasses { get; set; } = new List<SkipassRecord>();

    public IReadOnlyList<TarifficationRecord> Tariffications { get; set; } = new List<TarifficationRecord>();
}

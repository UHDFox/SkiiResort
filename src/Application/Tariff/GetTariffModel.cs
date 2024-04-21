using SkiiResort.Domain.Entities.Skipass;
using SkiiResort.Domain.Entities.Tariffication;

namespace SkiiResort.Application.Tariff;

public sealed class GetTariffModel
{
    public GetTariffModel(Guid id, string name, double priceModifier, bool isVip)
    {
        Id = id;
        Name = name;
        PriceModifier = priceModifier;
        IsVip = isVip;
    }

    public Guid Id { get; set; }

    public string? Name { get; set; }

    public double PriceModifier { get; set; }

    public bool IsVip { get; set; }

    public IReadOnlyList<SkipassRecord> Skipasses { get; set; } = new List<SkipassRecord>();

    public IReadOnlyList<TarifficationRecord> Tariffications { get; set; } = new List<TarifficationRecord>();
}

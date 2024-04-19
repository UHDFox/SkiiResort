namespace SkiiResort.Application.Tariff;

public sealed class AddTariffModel
{
    public AddTariffModel(string name, double priceModifier, bool isVip)
    {
        Name = name;
        PriceModifier = priceModifier;
        IsVip = isVip;
    }

    public string Name { get; set; }

    public double PriceModifier { get; set; }

    public bool IsVip { get; set; }
}

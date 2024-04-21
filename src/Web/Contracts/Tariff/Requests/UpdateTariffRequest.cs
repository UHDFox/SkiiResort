namespace SkiiResort.Web.Contracts.Tariff.Requests;

public sealed class UpdateTariffRequest
{
    public UpdateTariffRequest(Guid id, string name, double priceModifier, bool isVip)
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
}

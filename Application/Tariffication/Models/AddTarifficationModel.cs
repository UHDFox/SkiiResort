namespace SkiiResort.Application.Tariffication.Models;

public sealed class AddTarifficationModel
{
    public AddTarifficationModel(double price, Guid tariffId, Guid locationId)
    {
        Price = price;
        TariffId = tariffId;
        LocationId = locationId;
    }

    public double Price { get; set; }

    public Guid TariffId { get; set; }

    public Guid LocationId { get; set; }
}

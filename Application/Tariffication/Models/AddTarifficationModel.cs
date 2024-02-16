namespace Application.Tariffication.Models;

public sealed class AddTarifficationModel
{
    public AddTarifficationModel(int price, Guid tariffId, Guid locationId)
    {
        Price = price;
        TariffId = tariffId;
        LocationId = locationId;
    }

    public int Price { get; set; }

    public Guid TariffId { get; set; }
    
    public Guid LocationId { get; set; }
}
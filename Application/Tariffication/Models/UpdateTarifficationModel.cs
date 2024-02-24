namespace Application.Tariffication.Models;

public class UpdateTarifficationModel
{
    public UpdateTarifficationModel(Guid id, double price, Guid tariffId, Guid locationId)
    {
        Id = id;
        Price = price;
        TariffId = tariffId;
        LocationId = locationId;
    }

    public Guid Id { get; set; }

    public double Price { get; set; }

    public Guid TariffId { get; set; }
    
    public Guid LocationId { get; set; }
}
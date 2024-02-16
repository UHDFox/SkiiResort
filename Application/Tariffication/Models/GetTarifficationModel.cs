using Domain.Entities.Location;
using Domain.Entities.Tariff;

namespace Application.Tariffication.Models;

public sealed class GetTarifficationModel
{
    public GetTarifficationModel(Guid id, int price, Guid tariffId, Guid locationId)
    {
        Id = id;
        Price = price;
        TariffId = tariffId;
        LocationId = locationId;
    }

    public Guid Id { get; set; }

    public int Price { get; set; }

    public Guid TariffId { get; set; }

    public TariffRecord? Tariff { get; set; }
    
    public Guid LocationId { get; set; }
    
    public LocationRecord? Location { get; set; }
}
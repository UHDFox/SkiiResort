using Domain.Entities.Location;
using Domain.Entities.Tariff;

namespace Domain.Entities.Tariffication;

public sealed class TarifficationRecord
{
    public TarifficationRecord(int price, Guid tariffId, Guid locationId)
    {
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
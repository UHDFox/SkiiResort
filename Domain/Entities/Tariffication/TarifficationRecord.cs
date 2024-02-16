using Domain.Entities.Location;
using Domain.Entities.Tariff;

namespace Domain.Entities.Tariffication;

public sealed class TarifficationRecord
{
    public TarifficationRecord(int price, Guid tariffId)
    {
        Price = price;
        TariffId = tariffId;
    }
    
    public Guid Id { get; set; }
    
    public int Price { get; set; }
    
    public Guid TariffId { get; set; }
    
    public TariffRecord? Tariff { get; set; }

    public IReadOnlyList<LocationRecord> Locations { get; set; } = new List<LocationRecord>();
}
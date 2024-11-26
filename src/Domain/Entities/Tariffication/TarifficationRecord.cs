using SkiiResort.Domain.Entities.Location;
using SkiiResort.Domain.Entities.Tariff;

namespace SkiiResort.Domain.Entities.Tariffication;

public sealed class TarifficationRecord : DataObject
{
    public TarifficationRecord(double price, Guid tariffId, Guid locationId)
    {
        Price = price;
        TariffId = tariffId;
        LocationId = locationId;
    }

    public double Price { get; set; }

    public Guid TariffId { get; set; }

    public TariffRecord? Tariff { get; set; }

    public Guid LocationId { get; set; }

    public LocationRecord? Location { get; set; }
}

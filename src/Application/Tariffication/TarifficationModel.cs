using Application;
using SkiiResort.Domain.Entities.Location;
using SkiiResort.Domain.Entities.Tariff;

namespace SkiiResort.Application.Tariffication;

public class TarifficationModel : ServiceModel
{
    public TarifficationModel() : base(Guid.Empty)
    {

    }

    public TarifficationModel(Guid id, double price, Guid tariffId, Guid locationId)
    : base(id)
    {
        Id = id;
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

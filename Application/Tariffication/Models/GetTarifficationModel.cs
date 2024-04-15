using SkiiResort.Domain.Entities.Location;
using SkiiResort.Domain.Entities.Tariff;

namespace SkiiResort.Application.Tariffication.Models;

public sealed class GetTarifficationModel
{
    public GetTarifficationModel(Guid id, double price, Guid tariffId, Guid locationId)
    {
        Id = id;
        Price = price;
        TariffId = tariffId;
        LocationId = locationId;
    }

    public Guid Id { get; set; }

    public double Price { get; set; }

    public Guid TariffId { get; set; }

    public TariffRecord? Tariff { get; set; }

    public Guid LocationId { get; set; }

    public LocationRecord? Location { get; set; }
}

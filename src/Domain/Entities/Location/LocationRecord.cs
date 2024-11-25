using SkiiResort.Domain.Entities.Tariffication;

namespace SkiiResort.Domain.Entities.Location;

public sealed class LocationRecord : DataObject
{
    public LocationRecord(string name)
    {
        Name = name;
    }
    public string Name { get; set; }

    public IReadOnlyList<TarifficationRecord> Tariffications { get; set; } = new List<TarifficationRecord>();
}

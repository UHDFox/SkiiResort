using Application;

namespace SkiiResort.Application.Location;

public class LocationModel : ServiceModel
{
    public LocationModel() : base(Guid.Empty)
    {

    }

    public LocationModel(Guid id, string name)
    : base(id)
    {
        Id = id;
        Name = name;
    }

    public string Name { get; set; } = "";
}

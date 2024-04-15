namespace SkiiResort.Application.Location.Models;

public sealed class GetLocationModel
{
    public GetLocationModel(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }
}

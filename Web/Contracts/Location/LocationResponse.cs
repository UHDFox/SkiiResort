namespace Web.Contracts.Location;

public sealed class LocationResponse
{
    public LocationResponse(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
    
    public Guid Id { get; set; }

    public string Name { get; set; }
}
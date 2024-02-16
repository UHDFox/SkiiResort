namespace Application.Location.Models;

public sealed class UpdateLocationModel
{
    public UpdateLocationModel(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
    
    public Guid Id { get; set; }

    public string Name { get; set; }
}
namespace Application.Location.Models;

public sealed class AddLocationModel
{
    public AddLocationModel(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
}
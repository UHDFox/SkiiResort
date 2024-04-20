namespace SkiiResort.Web.Contracts.Location.Requests;

public sealed class UpdateLocationRequest
{
    public UpdateLocationRequest(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }
}

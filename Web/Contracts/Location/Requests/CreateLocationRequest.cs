namespace Web.Contracts.Location.Requests;

public sealed class CreateLocationRequest
{
    public CreateLocationRequest(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
}

namespace Web.Contracts.Tariff.Requests;

public sealed class UpdateTariffRequest
{
    public UpdateTariffRequest(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; set; }
    public string? Name { get; set; }
}
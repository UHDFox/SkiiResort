namespace Web.Contracts.Tariff;

public sealed class TariffResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }

    public TariffResponse(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}
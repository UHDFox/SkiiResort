namespace Web.Contracts.Tariff;

public sealed class TariffResponse
{
    public TariffResponse(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; set; }
    public string? Name { get; set; }
}
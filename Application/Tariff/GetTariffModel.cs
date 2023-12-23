namespace Application.Tariff;

public sealed class GetTariffModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }

    public GetTariffModel(Guid id, string name)
    {
        Id = id;
        Name = Name;
    }
}
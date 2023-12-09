namespace Domain.Entities.Tariff;

public sealed class TariffRecord
{
    public TariffRecord(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
}
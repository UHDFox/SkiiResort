namespace Domain.Entities.Tariff;

public sealed class TariffRecord
{
    public TariffRecord(string name)
    {
        Name = name;
    }

    public Guid Id { get; set; }
    
    public string Name { get; set; }
}
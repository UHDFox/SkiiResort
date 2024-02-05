using Domain.Entities.Skipass;

namespace Domain.Entities.Tariff;

public sealed class TariffRecord
{
    public TariffRecord(string name)
    {
        Name = name;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public IReadOnlyList<SkipassRecord> Skipasses { get; set; } = new List<SkipassRecord>();
}
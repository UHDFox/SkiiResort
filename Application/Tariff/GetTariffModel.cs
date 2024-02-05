using Domain.Entities.Skipass;

namespace Application.Tariff;

public sealed class GetTariffModel
{
    public GetTariffModel(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; set; }

    public string? Name { get; set; }

    public ICollection<SkipassRecord> Skipasses { get; set; } = new List<SkipassRecord>();
}
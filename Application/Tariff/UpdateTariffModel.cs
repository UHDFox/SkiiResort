using Domain.Entities.Skipass;

namespace Application.Tariff;

public sealed class UpdateTariffModel
{
    public UpdateTariffModel(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; set; }
    public string? Name { get; set; }
}
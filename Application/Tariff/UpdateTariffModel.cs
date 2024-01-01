namespace Application.Tariff;

public sealed class UpdateTariffModel
{
    public UpdateTariffModel(string name)
    {
        Name = name;
    }

    public Guid Id { get; set; }
    
    public string? Name { get; set; }
}
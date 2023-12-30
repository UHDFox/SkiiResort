namespace Application.Tariff;

public sealed class AddTariffModel
{
    public AddTariffModel(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
}
namespace Application.Tariff;

public sealed class AddTariffModel
{
    //public Guid Id { get; set; }
    public string Name { get; set; }
    public AddTariffModel(string name)
    {

        Name = name;
    }
}


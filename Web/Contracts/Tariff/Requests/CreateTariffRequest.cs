namespace Web.Contracts.Tariff.Requests;

public sealed class CreateTariffRequest
{
    public CreateTariffRequest(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
}
namespace Web.Contracts.Tariffication.Requests;

public sealed class CreateTarifficationRequest
{
    public CreateTarifficationRequest(double price, Guid tariffId, Guid locationId)
    {
        Price = price;
        TariffId = tariffId;
        LocationId = locationId;
    }
    
    public double Price { get; set; }
    
    public Guid TariffId { get; set; }
    
    public Guid LocationId { get; set; }
}
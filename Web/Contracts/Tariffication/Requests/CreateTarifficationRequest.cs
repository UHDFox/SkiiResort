namespace Web.Contracts.Tariffication.Requests;

public sealed class CreateTarifficationRequest
{
    public CreateTarifficationRequest(int price, Guid tariffId, Guid locationId)
    {
        Price = price;
        TariffId = tariffId;
        LocationId = locationId;
    }
    
    public int Price { get; set; }
    
    public Guid TariffId { get; set; }
    
    public Guid LocationId { get; set; }
}
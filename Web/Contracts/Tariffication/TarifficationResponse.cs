namespace Web.Contracts.Tariffication;

public sealed class TarifficationResponse
{
    public TarifficationResponse(Guid id, int price, Guid tariffId, Guid locationId)
    {
        Id = id;
        Price = price;
        TariffId = tariffId;
        LocationId = locationId;
    }
    
    public Guid Id { get; set; }
    
    public int Price { get; set; }
    
    public Guid TariffId { get; set; }
    
    public Guid LocationId { get; set; }
}
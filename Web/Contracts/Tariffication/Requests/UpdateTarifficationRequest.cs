namespace Web.Contracts.Tariffication.Requests;

public class UpdateTarifficationRequest
{
    public UpdateTarifficationRequest(double price, Guid tariffId, Guid locationId)
    {
        Price = price;
        TariffId = tariffId;
        LocationId = locationId;
    }
    
    public Guid Id { get; set; }
    
    public double Price { get; set; }
    
    public Guid TariffId { get; set; }
    
    public Guid LocationId { get; set; }
}
using Domain.Entities.Tariff;
using Domain.Entities.Visitor;

namespace Application.Skipass;

public sealed class UpdateSkipassModel
{
    public UpdateSkipassModel(Guid id, Guid tariffId, Guid visitorId, bool status)
    {
        Id = id;
        TariffId = tariffId;
        VisitorId = visitorId;
        Status = status;
    }
    public Guid Id { get; set; }
    
    public int Balance { get; set; }
    
    public Guid TariffId { get; set; }
    
    public Guid VisitorId { get; set; }
    
    public bool Status { get; set; }
}
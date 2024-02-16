using Domain.Entities.Tariffication;
using Domain.Entities.VisitorsAction;

namespace Domain.Entities.Location;

public sealed class LocationRecord
{
    public LocationRecord(string name, Guid tarifficationId, Guid visitorActionsId)
    {
        Name = name;
        TarifficationId = tarifficationId;
        VisitorActionsId = visitorActionsId;
    }
    
    public Guid Id { get; set; }

    public string Name { get; set; }
    
    public Guid TarifficationId { get; set; }
    
    public TarifficationRecord? Tariffication { get; set; }
    
    public Guid VisitorActionsId { get; set; }
    
    public VisitorActionsRecord? VisitorActions { get; set; }
}
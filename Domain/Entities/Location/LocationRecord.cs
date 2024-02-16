using Domain.Entities.Tariffication;
using Domain.Entities.VisitorsAction;

namespace Domain.Entities.Location;

public sealed class LocationRecord
{
    public LocationRecord(Guid id, string name, Guid tarifficationId)
    {
        Id = id;
        Name = name;
        TarifficationId = tarifficationId;
    }
    
    public Guid Id { get; set; }

    public string Name { get; set; }
    
    public Guid TarifficationId { get; set; }
    
    public TarifficationRecord? Tariffication { get; set; }
    
    public VisitorActionsRecord? VisitorActions { get; set; }
}
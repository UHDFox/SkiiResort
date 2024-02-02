using Domain.Entities.Skipass;
using Domain.Enums;
namespace Domain.Entities.VisitorsAction;

public class VisitorActionsRecord
{
    public VisitorActionsRecord(Guid id, Guid skipassId, Place place, DateTime time, int balanceChange,
        ActionType typeOfAction)
    {
        Id = id;
        SkipassId = skipassId;
        Place = place;
        Time = time;
        BalanceChange = balanceChange;
        TypeOfAction = typeOfAction;
    }

    

    public Guid Id { get; set; }
    
    public Guid SkipassId { get; set; }
    
    public SkipassRecord? SkipassRecord { get; set; }
    
    public Place Place { get; set; }
    
    public DateTime Time { get; set; }
    
    public int BalanceChange { get; set; }

    public ActionType TypeOfAction { get; set; }
}
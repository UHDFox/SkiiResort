using Domain.Entities.Skipass;
using Domain.Enums;

namespace Application.VisitorAction;

public sealed class AddVisitorActionsModel
{
    public AddVisitorActionsModel(Guid skipassId, Place place, DateTime time, int balanceChange,
        ActionType typeOfAction)
    {
        SkipassId = skipassId;
        Place = place;
        Time = time;
        BalanceChange = balanceChange;
        TypeOfAction = typeOfAction;
    }
    
    public Guid SkipassId { get; set; }
    
    public SkipassRecord? Skipass{ get; set; }
    
    public Place Place { get; set; }
    
    public DateTime Time { get; set; }
    
    public int BalanceChange { get; set; }

    public ActionType TypeOfAction { get; set; }
}
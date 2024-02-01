using Domain.Entities.Skipass;
using Domain.Enums;

namespace Application.VisitorAction;

public class UpdateVisitorActionsModel
{
    public UpdateVisitorActionsModel(Guid id,Guid skipassId, Place place, DateTime time, int balanceChange,
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
    
    public SkipassRecord? Skipass{ get; set; }
    
    public Place Place { get; set; }
    
    public DateTime Time { get; set; }
    
    public int BalanceChange { get; set; }

    public ActionType TypeOfAction { get; set; }
}
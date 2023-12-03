using Domain.Entities.Skipass;
using Domain.Enums;

namespace Domain.Entities.VisitorsAction;

public class VisitorsActions
{
    public VisitorsActions(Guid id, int skipassId, string place, DateTime time, int balanceChange,
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
    public int SkipassId { get; set; }
    public SkipassRecord SkipassRecord { get; set; }
    public string Place { get; set; }
    public DateTime Time { get; set; }
    public int BalanceChange { get; set; }

    public ActionType TypeOfAction { get; set; }
}
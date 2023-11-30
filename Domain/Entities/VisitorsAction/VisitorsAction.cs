using Domain.Enums;
using Domain.Entities.SkipassEntity;
namespace Domain.Entities.VisitorsAction;

public class VisitorsActions
{
    
    public int Id { get; set; }
    public int SkipassId { get; set; }
    public Skipass Skipass { get; set; }
    public string Place { get; set; }
    public DateTime Time { get; set; }
    public int BalanceChange { get; set; }

    public ActionType ActionType
    {
        get;
        set;
    }

}
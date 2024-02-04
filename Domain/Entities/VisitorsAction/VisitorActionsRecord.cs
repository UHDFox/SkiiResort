using System;
using Domain.Entities.Skipass;
using Domain.Enums;
namespace Domain.Entities.VisitorsAction;

public class VisitorActionsRecord
{
    public VisitorActionsRecord(Guid skipassId, Place place, DateTime time, int balanceChange, bool isVip)
    {
        SkipassId = skipassId;
        Place = place;
        Time = time;
        BalanceChange = balanceChange;
        IsVip = isVip;
    }

    
    public Guid Id { get; set; }
    
    public Guid SkipassId { get; set; }
    
    public SkipassRecord? Skipass { get; set; }
    
    public Place Place { get; set; }
    
    public DateTime Time { get; set; }
    
    public int BalanceChange { get; set; }

    public bool IsVip { get; set; }
}
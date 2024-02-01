using Domain.Entities.Tariff;
using Domain.Entities.Visitor;

namespace Domain.Entities.Skipass;

public sealed class SkipassRecord
{
    public SkipassRecord(Guid id, int balance, Guid tariffId, TariffRecord tariffRecord, Guid visitorId,VisitorRecord visitorRecord, bool status)
    {
        Id = id;
        Balance = balance;
        TariffId = tariffId;
        Tariff = tariffRecord;
        VisitorId = visitorId;
        Visitor = visitorRecord;
        Status = status;
    }

    public SkipassRecord() {}   //empty default constructor made to prevent EF silliness

    public Guid Id { get; set; }
    
    public int Balance { get; set; }
    
    public Guid TariffId { get; set; }
    
    public TariffRecord? Tariff { get; set; }
    
    public Guid VisitorId { get; set; }
    
    public VisitorRecord? Visitor{ get; set; }
    
    public bool Status { get; set; }
}
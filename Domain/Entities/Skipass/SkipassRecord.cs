public sealed class SkipassRecord
{
    public SkipassRecord(int balance, Guid tariffId, Guid visitorId, bool status)
    {
        Balance = balance;
        TariffId = tariffId;
        VisitorId = visitorId;
        Status = status;
    }

    public Guid Id { get; set; }
    
    public int Balance { get; set; }
    
    public Guid TariffId { get; set; }
    
    public TariffRecord? Tariff { get; set; }
    
    public Guid VisitorId { get; set; }
    
    public VisitorRecord? Visitor{ get; set; }
    
    public bool Status { get; set; }

    public ICollection<VisitorActionsRecord> VisitorActions { get; set; } = new List<VisitorActionsRecord>(); 
}
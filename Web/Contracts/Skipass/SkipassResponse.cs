using Application.Tariff;
using Application.Visitor;
using Domain.Entities.Tariff;
using Domain.Entities.Visitor;

namespace Web.Contracts.Skipass;

public sealed class SkipassResponse
{
    public SkipassResponse(){}

    public SkipassResponse(Guid id, int balance, Guid tariffId, TariffRecord tariffRecord, Guid visitorId,VisitorRecord visitorRecord, bool status)
    {
        Id = id;
        Balance = balance;
        TariffId = tariffId;
        Tariff = tariffRecord;
        VisitorId = visitorId;
        Visitor = visitorRecord;
        Status = status;
    }
    

    public Guid Id { get; set; }
    public int Balance { get; set; }
    public Guid TariffId { get; set; }
    public TariffRecord? Tariff { get; set; }
    public Guid VisitorId { get; set; }
    public VisitorRecord? Visitor { get; set; }
    public bool Status { get; set; }
}
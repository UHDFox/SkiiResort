using FluentAssertions;
using FluentAssertions.Execution;
using SkiiResort.Application.VisitorAction;
using SkiiResort.Domain.Entities.VisitorsAction;
using SkiiResort.Domain.Enums;

namespace SkiiResort.Tests.VisitorActions;

public static class VisitorActionsExtensions
{
    public static bool VerifyBy(this VisitorActionsRecord record, VisitorActionsRecord model)
    {
        using (var scope = new AssertionScope())
        {
            record.Id.Should().Be(model.Id);
            record.LocationId.Should().Be(model.LocationId);
            record.SkipassId.Should().Be(model.SkipassId);
            record.Time.Should().Be(model.Time);
            record.BalanceChange.Should().Be(model.BalanceChange);
        }

        return true;
    }

    public static AddVisitorActionsModel ToAddModel(this VisitorActionsRecord record) =>
        new AddVisitorActionsModel(record.SkipassId, record.LocationId);

    public static VisitorActionsRecord ToEntity(this AddVisitorActionsModel model) =>
        new VisitorActionsRecord(model.SkipassId, model.LocationId, (DateTimeOffset)model.Time!,
            (double)model.BalanceChange!, (OperationType)model.TransactionType!);

    public static GetVisitorActionsModel ToGetModel(this VisitorActionsRecord record) =>
        new GetVisitorActionsModel(record.Id, record.SkipassId, record.LocationId, record.Time,
            record.BalanceChange, record.TransactionType);

    public static VisitorActionsRecord ToEntity(this UpdateVisitorActionsModel model) =>
        new VisitorActionsRecord(model.SkipassId, model.LocationId, (DateTimeOffset)model.Time!,
            (double)model.BalanceChange!, (OperationType)model.TransactionType!);
}

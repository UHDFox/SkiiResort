using FluentAssertions;
using FluentAssertions.Execution;
using SkiiResort.Application.Skipass;
using SkiiResort.Domain.Entities.Skipass;

namespace SkiiResort.Tests.Skipass;

public static class SkipassExtensions
{
    public static bool VerifyBy(this SkipassRecord record, UpdateSkipassModel model)
    {
        using (var scope = new AssertionScope())
        {
            record.Balance.Should().Be(model.Balance);
            record.TariffId.Should().Be(model.TariffId);
            record.VisitorId.Should().Be(model.VisitorId);
            record.Status.Should().Be(model.Status);

            if (scope.HasFailures())
            {
                return false;
            }
        }

        return true;
    }

    public static bool VerifyBy(this SkipassRecord record, AddSkipassModel model)
    {
        using (var scope = new AssertionScope())
        {
            record.Balance.Should().Be(model.Balance);
            record.TariffId.Should().Be(model.TariffId);
            record.VisitorId.Should().Be(model.VisitorId);
            record.Status.Should().Be(model.Status);

            if (scope.HasFailures())
            {
                return false;
            }
        }

        return true;
    }

    public static bool VerifyBy(this SkipassRecord record, SkipassRecord model)
    {
        using (var scope = new AssertionScope())
        {
            record.Balance.Should().Be(model.Balance);
            record.TariffId.Should().Be(model.TariffId);
            record.VisitorId.Should().Be(model.VisitorId);
            record.Status.Should().Be(model.Status);

            if (scope.HasFailures())
            {
                return false;
            }
        }

        return true;
    }

    public static AddSkipassModel ToAddModel(this SkipassRecord record) =>
        new AddSkipassModel(record.Balance, record.TariffId, record.VisitorId, record.Status);

    public static SkipassRecord ToEntity(this AddSkipassModel model) =>
        new SkipassRecord(model.Balance, model.TariffId, model.VisitorId, model.Status);

    public static UpdateSkipassModel ToUpdateModel(this SkipassRecord record) =>
        new UpdateSkipassModel(record.Id, record.Balance, record.TariffId, record.VisitorId, record.Status);

    public static SkipassRecord ToEntity(this UpdateSkipassModel model) =>
        new SkipassRecord(model.Balance, model.TariffId, model.VisitorId, model.Status);

    public static UpdateSkipassModel ToGetModel(this SkipassRecord record) =>
        new UpdateSkipassModel(record.Id, record.Balance, record.TariffId, record.VisitorId, record.Status);
}

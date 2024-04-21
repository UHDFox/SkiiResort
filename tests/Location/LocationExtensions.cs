using FluentAssertions;
using FluentAssertions.Execution;
using SkiiResort.Application.Location.Models;
using SkiiResort.Domain.Entities.Location;

namespace SkiiResort.Tests.Location;

public static class LocationExtensions
{
    public static bool VerifyBy(this LocationRecord record, UpdateLocationModel model)
    {
        using (var scope = new AssertionScope())
        {
            record.Id.Should().Be(model.Id);
            record.Name.Should().Be(model.Name);

            if (scope.HasFailures())
            {
                return false;
            }
        }

        return true;
    }

    public static bool VerifyBy(this LocationRecord record, AddLocationModel model)
    {
        using (var scope = new AssertionScope())
        {
            record.Name.Should().Be(model.Name);

            if (scope.HasFailures())
            {
                return false;
            }
        }

        return true;
    }


    public static AddLocationModel ToAddModel(this LocationRecord record) =>
        new AddLocationModel(record.Name);

    public static LocationRecord ToEntity(this AddLocationModel model) =>
        new LocationRecord(model.Name);

    public static UpdateLocationModel ToUpdateModel(this LocationRecord record) =>
        new UpdateLocationModel(record.Id, record.Name);

    public static LocationRecord ToEntity(this UpdateLocationModel model) =>
        new LocationRecord(model.Name);

    public static UpdateLocationModel ToGetModel(this LocationRecord record) =>
        new UpdateLocationModel(record.Id, record.Name);
}

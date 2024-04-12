using Application.Tariff;
using Domain.Entities.Tariff;
using FluentAssertions;
using FluentAssertions.Execution;

namespace Tests.Tariff;

public static class TariffExtensions
{
    public static bool VerifyBy(this TariffRecord record, UpdateTariffModel model)
    {
        using (var scope = new AssertionScope())
        {
            record.Id.Should().Be(model.Id);
            record.Name.Should().Be(model.Name);
            record.PriceModifier.Should().Be(model.PriceModifier);
            record.IsVip.Should().Be(model.IsVip);

            if (scope.HasFailures())
            {
                return false;
            }
        }

        return true;
    }

    public static bool VerifyBy(this TariffRecord record, AddTariffModel model)
    {
        using (var scope = new AssertionScope())
        {
            record.Name.Should().Be(model.Name);
            record.PriceModifier.Should().Be(model.PriceModifier);
            record.IsVip.Should().Be(model.IsVip);

            if (scope.HasFailures())
            {
                return false;
            }
        }

        return true;
    }


    public static AddTariffModel ToAddModel(this TariffRecord record) =>
        new AddTariffModel(record.Name, record.PriceModifier, record.IsVip);

    public static TariffRecord ToEntity(this AddTariffModel model) =>
        new TariffRecord(model.Name, model.PriceModifier, model.IsVip);

    public static UpdateTariffModel ToUpdateModel(this TariffRecord record) =>
        new UpdateTariffModel(record.Id, record.Name, record.PriceModifier, record.IsVip);

    public static TariffRecord ToEntity(this UpdateTariffModel model) =>
        new TariffRecord(model.Name!, model.PriceModifier, model.IsVip);

    public static UpdateTariffModel ToGetModel(this TariffRecord record) =>
        new UpdateTariffModel(record.Id, record.Name, record.PriceModifier, record.IsVip);
}

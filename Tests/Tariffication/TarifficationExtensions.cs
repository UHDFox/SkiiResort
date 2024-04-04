using Application.Tariffication.Models;
using Domain.Entities.Tariffication;
using FluentAssertions;
using FluentAssertions.Execution;

namespace Tests.Tariffication;

public static class TarifficationExtensions
{
    public static bool VerifyBy(this TarifficationRecord record, UpdateTarifficationModel model)
    {
        using (var scope = new AssertionScope())
        {
            record.Id.Should().Be(model.Id);
            record.TariffId.Should().Be(model.TariffId);
            record.LocationId.Should().Be(model.LocationId);
            record.Price.Should().Be(model.Price);
            
            if (scope.HasFailures()) return false;
        }

        return true;
    }
    
    public static bool VerifyBy(this TarifficationRecord record, AddTarifficationModel model)
    {
        using (var scope = new AssertionScope())
        {
            record.TariffId.Should().Be(model.TariffId);
            record.LocationId.Should().Be(model.LocationId);
            record.Price.Should().Be(model.Price);
            
            if (scope.HasFailures()) return false;
        }

        return true;
    }


    public static AddTarifficationModel ToAddModel(this TarifficationRecord record) =>
        new AddTarifficationModel(record.Price, record.TariffId, record.LocationId);
    
    public static TarifficationRecord ToEntity(this AddTarifficationModel model) =>
        new TarifficationRecord(model.Price, model.TariffId, model.LocationId);
    
    public static UpdateTarifficationModel ToUpdateModel(this TarifficationRecord record) => 
        new UpdateTarifficationModel(record.Id, record.Price, record.TariffId, record.LocationId);

    public static TarifficationRecord ToEntity(this UpdateTarifficationModel model) =>
        new TarifficationRecord(model.Price, model.TariffId, model.LocationId);
    
    public static UpdateTarifficationModel ToGetModel(this TarifficationRecord record) => 
        new UpdateTarifficationModel(record.Id, record.Price, record.TariffId, record.LocationId);
}
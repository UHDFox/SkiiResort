using Application.Visitor;
using Domain.Entities.Visitor;
using FluentAssertions;
using FluentAssertions.Execution;

namespace Tests.Visitor;

public static class VisitorExtensions
{
    public static bool VerifyBy(this VisitorRecord record, UpdateVisitorModel model)
    {
        using (var scope = new AssertionScope())
        {
            record.Name.Should().Be(model.Name);
            record.Birthdate.Should().Be(model.Birthdate);
            record.Phone.Should().Be(model.Phone);
            record.Passport.Should().Be(model.Passport);
            if (scope.HasFailures()) return false;
        }

        return true;
    }
    
    public static bool VerifyBy(this VisitorRecord record, AddVisitorModel model)
    {
        using (var scope = new AssertionScope())
        {
            record.Name.Should().Be(model.Name);
            record.Birthdate.Should().Be(model.Birthdate);
            record.Phone.Should().Be(model.Phone);
            record.Passport.Should().Be(model.Passport);
            if (scope.HasFailures()) return false;
        }

        return true;
    }
    
    
    public static AddVisitorModel ToAddModel(this VisitorRecord record) => 
        new AddVisitorModel(record.Name, record.Age, record.Phone, record.Passport);
    
    public static VisitorRecord ToEntity(this AddVisitorModel model) =>
        new VisitorRecord(model.Name!, model.Age, model.Phone, model.Birthdate, model.Passport!);
    
    public static UpdateVisitorModel ToUpdateModel(this VisitorRecord record) => 
        new UpdateVisitorModel(record.Id, record.Name, record.Age, record.Phone, record.Birthdate, record.Passport);

    public static VisitorRecord ToEntity(this UpdateVisitorModel model) =>
        new VisitorRecord(model.Name!, model.Age, model.Phone, model.Birthdate, model.Passport);
    
    public static UpdateVisitorModel ToGetModel(this VisitorRecord record) => 
        new UpdateVisitorModel(record.Id, record.Name, record.Age, record.Phone, record.Birthdate, record.Passport);
}

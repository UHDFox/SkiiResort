using Application;
using SkiiResort.Domain.Entities.Skipass;

namespace SkiiResort.Application.Visitor;

public sealed class VisitorModel : ServiceModel
{
    public VisitorModel() : base(Guid.Empty)
    {
    }

    public VisitorModel(Guid id, string name, int age, string phone, DateTimeOffset birthdate, string passport, Guid userId)
    : base(id)
    {
        Id = id;
        Name = name;
        Age = age;
        Phone = phone;
        Birthdate = birthdate;
        Passport = passport;
        UserId = userId;
    }
    public string? Name { get; set; }

    public int Age { get; set; }

    public string Phone { get; set; } = "";

    public DateTimeOffset Birthdate { get; set; }

    public string? Passport { get; set; } = "";

    public IReadOnlyList<SkipassRecord> Skipasses { get; set; } = new List<SkipassRecord>();

    public Guid UserId { get; set; }
}

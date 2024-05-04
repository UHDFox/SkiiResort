using SkiiResort.Domain.Entities.Skipass;

namespace SkiiResort.Domain.Entities.Visitor;

public sealed class VisitorRecord
{
    public VisitorRecord(string name, int age, string phone, DateTimeOffset birthdate, string passport)
    {
        Name = name;
        Age = age;
        Phone = phone;
        Birthdate = birthdate;
        Passport = passport;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public int Age { get; set; }

    public string Phone { get; set; }

    public DateTimeOffset Birthdate { get; set; }

    public string Passport { get; set; }

    public IReadOnlyList<SkipassRecord> Skipasses { get; set; } = new List<SkipassRecord>();
}

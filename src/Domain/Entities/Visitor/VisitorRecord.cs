using SkiiResort.Domain.Entities.Skipass;
using SkiiResort.Domain.Entities.User;

namespace SkiiResort.Domain.Entities.Visitor;

public sealed class VisitorRecord : DataObject
{
    public VisitorRecord(string name,
        int age,
        string phone,
        DateTimeOffset birthdate,
        string passport,
        Guid userId)
    {
        Name = name;
        Age = age;
        Phone = phone;
        Birthdate = birthdate;
        Passport = passport;
        UserId = userId;
    }

    public string Name { get; set; }

    public int Age { get; set; }

    public string Phone { get; set; }

    public DateTimeOffset Birthdate { get; set; }

    public string Passport { get; set; }

    public IReadOnlyList<SkipassRecord> Skipasses { get; set; } = new List<SkipassRecord>();

    public Guid UserId { get; set; }

    public UserRecord? User { get; set; }
}

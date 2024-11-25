namespace SkiiResort.Web.Contracts.Visitor;

public sealed class VisitorResponse
{
    public VisitorResponse(Guid id, string name, int age, string phone, DateTimeOffset birthdate, string passport, Guid userId)
    {
        Id = id;
        Name = name;
        Age = age;
        Phone = phone;
        Birthdate = birthdate;
        Passport = passport;
        UserId = userId;
    }

    public Guid Id { get; set; }

    public string? Name { get; set; }

    public int Age { get; set; }

    public string Phone { get; set; }

    public DateTimeOffset Birthdate { get; set; }

    public string? Passport { get; set; }

    public Guid UserId { get; set; }
}

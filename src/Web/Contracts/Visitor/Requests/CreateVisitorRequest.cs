namespace SkiiResort.Web.Contracts.Visitor.Requests;

public sealed class CreateVisitorRequest
{
    public CreateVisitorRequest(string name, int age, string phone, string passport, Guid userId)
    {
        Name = name;
        Age = age;
        Phone = phone;
        Passport = passport;
        UserId = userId;
    }

    public string? Name { get; set; }

    public int Age { get; set; }

    public string Phone { get; set; }

    public DateTimeOffset Birthdate { get; set; }
    public string? Passport { get; set; }

    public Guid UserId { get; set; }
}

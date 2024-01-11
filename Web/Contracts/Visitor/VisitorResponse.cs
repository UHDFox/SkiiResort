namespace Web.Contracts.Visitor;

public class VisitorResponse
{
    public VisitorResponse(Guid id, string name, int age, int phone, DateTime birthdate, string passport)
    {
        Id = id;
        Name = name;
        Age = age;
        Phone = phone;
        Birthdate = birthdate;
        Passport = passport;
    }
    public Guid Id { get; set; }
    
    public string? Name { get; set; }
    
    public int Age { get; set; }
    
    public int Phone { get; set; }

    public DateTime Birthdate { get; set; }
    
    public string? Passport { get; set; }
}
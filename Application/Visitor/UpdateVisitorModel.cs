namespace Application.Visitor;

public sealed class UpdateVisitorModel
{
    public UpdateVisitorModel(string name, int age, int phone, DateTime birthdate, string passport)
    {
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
namespace Application.Visitor;

public sealed class AddVisitorModel
{
    public AddVisitorModel(string name, int age, int phone, string passport)
    {
        Name = name;
        Age = age;
        Phone = phone;
        Passport = passport;
    }
    public string? Name { get; set; }
    
    
    public int Age { get; set; }
    
    
    public int Phone { get; set; }
    

    public DateTime Birthdate { get; set; }
    
    
    public string? Passport { get; set; }
}
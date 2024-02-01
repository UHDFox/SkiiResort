
using Domain.Entities.Skipass;

namespace Application.Visitor;

public sealed class GetVisitorModel
{
    public GetVisitorModel(Guid id, string name, int age, string phone, DateTime birthdate, string passport)
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
    
    
    public string Phone { get; set; }
    

    public DateTime Birthdate { get; set; }
    
    
    public string? Passport { get; set; }
    
    public ICollection<SkipassRecord> Skipasses { get; set; } = new List<SkipassRecord>();
}
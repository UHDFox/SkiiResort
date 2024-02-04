using System;
using System.Collections.Generic;
using Domain.Entities.Skipass;

namespace Domain.Entities.Visitor;

public sealed class VisitorRecord
{
    public VisitorRecord(string name, int age, string phone, DateTime birthdate, string passport)
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

    public DateTime Birthdate { get; set; }
    
    public string Passport { get; set; }

    public ICollection<SkipassRecord> Skipasses { get; set; } = new List<SkipassRecord>();
}
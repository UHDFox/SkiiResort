using System;
using System.ComponentModel.DataAnnotations;


namespace Domain
{
    public sealed class Visitor
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        [Phone] 
        public int Phone { get; set; }
        public DateTime Birthdate { get; set; }
    }
}


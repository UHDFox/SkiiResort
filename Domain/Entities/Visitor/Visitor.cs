using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Visitor
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


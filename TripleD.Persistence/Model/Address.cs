using System.ComponentModel.DataAnnotations;

namespace TripleD.Persistence.Model
{
    public class Address
    {
        public string City { get; set; }

        [Key]
        public int Id { get; set; }

        public string Number { get; set; }

        public string Street { get; set; }
    }
}
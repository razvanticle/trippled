using System.ComponentModel.DataAnnotations;

namespace TripleD.Persistence.Model
{
    public class Service
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Rating { get; set; }
    }
}
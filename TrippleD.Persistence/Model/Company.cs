using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrippleD.Persistence.Model
{
    public class Company
    {
        public Address Address { get; set; }

        public DateTime CloseTime { get; set; }

        public string Email { get; set; }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime OpenTime { get; set; }

        public string PhoneNumber { get; set; }

        public decimal Rating { get; set; }

        public IEnumerable<Service> Services { get; set; }

        public string WebSite { get; set; }
    }
}
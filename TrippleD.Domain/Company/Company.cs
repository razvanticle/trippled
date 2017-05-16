using System.Collections.Generic;
using TrippleD.Domain.SharedKernel;

namespace TrippleD.Domain.Company
{
    public class Company : Entity<int>
    {
        public Company(int id) : base(id)
        {
        }

        public Address Address { get; set; }

        public TimeInterval BusinessHours { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public decimal Rating { get; set; }

        public IEnumerable<Service> Services { get; set; }

        public string WebSite { get; set; }
    }
}
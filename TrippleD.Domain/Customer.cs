using System.Collections.Generic;
using TrippleD.Domain.SharedKernel;

namespace TrippleD.Domain
{
    public class Customer : Entity<int>
    {
        public Customer(int id) : base(id)
        {
        }

        public string Email { get; set; }

        public string MobilePhone { get; set; }

        public Name Name { get; set; }

        public IEnumerable<Order> Orders { get; set; }
    }
}
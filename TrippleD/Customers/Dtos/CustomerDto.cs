using System;
using System.Collections.Generic;

namespace TrippleD.Customers.Dtos
{
    public class CustomerDto
    {
        public string Email { get; set; }

        public Guid Id { get; set; }

        public string MobilePhone { get; set; }

        public string Name { get; set; }

        public IEnumerable<OrderDto> Orders { get; set; }
    }
}
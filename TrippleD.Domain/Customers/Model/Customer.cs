using System.Collections.Generic;
using TrippleD.Core;
using TrippleD.Domain.Customers.Events;
using TrippleD.Domain.SharedKernel;

namespace TrippleD.Domain.Customers.Model
{
    public class Customer : AggregateRoot<int>
    {
        public Customer(int id) : base(id)
        {
        }

        public string Email { get; set; }

        public string MobilePhone { get; set; }

        public Name Name { get; set; }

        public IList<Order> Orders { get; set; }

        public void AddOrder(Order order)
        {
            Guard.ArgNotNull(order, nameof(order));

            Orders.Add(order);

            AddEvent(new NewOrderEvent(order));
        }
    }
}
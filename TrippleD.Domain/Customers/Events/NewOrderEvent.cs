using TrippleD.Sales.Domain.Customers.Model;
using TrippleD.SharedKernel.Events;

namespace TrippleD.Sales.Domain.Customers.Events
{
    public class NewOrderEvent : DomainEvent
    {
        public NewOrderEvent(Order order)
        {
            Order = order;
        }

        public Order Order { get; }
    }
}
using TrippleD.Domain.Companies.Model;
using TrippleD.SharedKernel;
using TrippleD.SharedKernel.Model;

namespace TrippleD.Domain.Customers.Model
{
    public class OrderItem : ValueObjectBase<OrderItem>
    {
        public OrderItem(Service service, Address executionAddress)
        {
            Service = service;
            ExecutionAddress = executionAddress;
        }

        public Address ExecutionAddress { get; }

        public Service Service { get; }
    }
}
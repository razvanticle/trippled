using TrippleD.Core;
using TrippleD.Persistence.Repository;
using TrippleD.Sales.Domain.Customers.Model;
using TrippleD.SharedKernel.EventDispatcher;

namespace TrippleD.Sales.Persistence
{
    [Service(typeof(IEntityRepository<Customer>))]
    public class CustomerRepository:EntityRepository<Customer>
    {
        public CustomerRepository(TrippleD.Persistence.InMemoryStore.InMemoryStore store, IDomainEventDispatcher dispatcher) : base(store, dispatcher)
        {
        }
    }
}
using TrippleD.Core;
using TrippleD.Domain.Customers.Model;
using TrippleD.Persistence.Repository;
using TrippleD.SharedKernel.EventDispatcher;

namespace TrippleD.Persistence
{
    [Service(typeof(IEntityRepository<Customer>))]
    public class CustomerRepository:EntityRepository<Customer>
    {
        public CustomerRepository(TrippleD.Persistence.InMemoryStore.InMemoryStore store, IDomainEventDispatcher dispatcher) : base(store, dispatcher)
        {
        }
    }
}
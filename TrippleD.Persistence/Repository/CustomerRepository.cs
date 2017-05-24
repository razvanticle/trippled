using TrippleD.Core;
using TrippleD.Domain.Customers.Model;
using TrippleD.Domain.SharedKernel.EventDispatcher;

namespace TrippleD.Persistence.Repository
{
    [Service(typeof(IEntityRepository<Customer,int>))]
    public class CustomerRepository:EntityRepository<Customer,int>
    {
        public CustomerRepository(InMemoryStore.InMemoryStore store, IDomainEventDispatcher dispatcher) : base(store, dispatcher)
        {
        }
    }
}
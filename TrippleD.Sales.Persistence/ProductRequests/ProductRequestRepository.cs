using TrippleD.Core;
using TrippleD.Domain.ProductRequests;
using TrippleD.Domain.SharedKernel.EventDispatcher;
using TrippleD.Persistence.Repository;

namespace TrippleD.Persistence.ProductRequests
{
    [Service(typeof(IEntityRepository<ProductRequest>))]
    public class ProductRequestRepository : EntityRepository<ProductRequest>
    {
        public ProductRequestRepository(InMemoryStore.InMemoryStore store,
            IDomainEventDispatcher dispatcher) : base(store, dispatcher)
        {
        }
    }
}
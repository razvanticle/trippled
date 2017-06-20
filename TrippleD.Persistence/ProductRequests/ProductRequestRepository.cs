using TrippleD.Core;
using TrippleD.Domain.ProductRequests;
using TrippleD.Persistence.Repository;
using TrippleD.SharedKernel.EventDispatcher;

namespace TrippleD.Persistence.ProductRequests
{
    [Service(typeof(IEntityRepository<ProductRequest>))]
    public class ProductRequestRepository : EntityRepository<ProductRequest>
    {
        public ProductRequestRepository(TrippleD.Persistence.InMemoryStore.InMemoryStore store,
            IDomainEventDispatcher dispatcher) : base(store, dispatcher)
        {
        }
    }
}
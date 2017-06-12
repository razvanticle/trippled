using TrippleD.Core;
using TrippleD.Persistence.Repository;
using TrippleD.Sales.Domain.ProductRequests;
using TrippleD.SharedKernel.EventDispatcher;

namespace TrippleD.Sales.Persistence.ProductRequests
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
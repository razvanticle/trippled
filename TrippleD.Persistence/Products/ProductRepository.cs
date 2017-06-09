using TrippleD.Core;
using TrippleD.Domain.Products;
using TrippleD.Domain.SharedKernel.EventDispatcher;
using TrippleD.Persistence.Repository;

namespace TrippleD.Persistence.Products
{
    [Service(typeof(IEntityRepository<Product>))]
    public class ProductRepository : EntityRepository<Product>
    {
        public ProductRepository(InMemoryStore.InMemoryStore store, IDomainEventDispatcher dispatcher) : base(store,
            dispatcher)
        {
        }
    }
}
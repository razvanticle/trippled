using TrippleD.Core;
using TrippleD.Domain.Products;
using TrippleD.Persistence.Repository;
using TrippleD.SharedKernel.EventDispatcher;

namespace TrippleD.Persistence.Products
{
    [Service(typeof(IEntityRepository<Product>))]
    public class ProductRepository : EntityRepository<Product>
    {
        public ProductRepository(TrippleD.Persistence.InMemoryStore.InMemoryStore store, IDomainEventDispatcher dispatcher) : base(store,
            dispatcher)
        {
        }
    }
}
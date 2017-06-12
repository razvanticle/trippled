using TrippleD.Core;
using TrippleD.Persistence.Repository;
using TrippleD.Sales.Domain.Products;
using TrippleD.SharedKernel.EventDispatcher;

namespace TrippleD.Sales.Persistence.Products
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
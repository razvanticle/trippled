using TrippleD.Core;
using TrippleD.Persistence.InMemoryStore;
using TrippleD.Persistence.Repository;
using TrippleD.Sales.Domain.Companies.Model;
using TrippleD.SharedKernel.EventDispatcher;

namespace TrippleD.Sales.Persistence
{
    [Service(typeof(IEntityRepository<Company>))]
    public class CompanyRepository : EntityRepository<Company>
    {
        public CompanyRepository(InMemoryStore store, IDomainEventDispatcher dispatcher) : base(store, dispatcher)
        {
        }
    }
}
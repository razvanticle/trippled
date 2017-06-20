using TrippleD.Core;
using TrippleD.Domain.Companies.Model;
using TrippleD.Persistence.Repository;
using TrippleD.SharedKernel.EventDispatcher;

namespace TrippleD.Persistence
{
    [Service(typeof(IEntityRepository<Company>))]
    public class CompanyRepository : EntityRepository<Company>
    {
        public CompanyRepository(InMemoryStore.InMemoryStore store, IDomainEventDispatcher dispatcher) : base(store, dispatcher)
        {
        }
    }
}
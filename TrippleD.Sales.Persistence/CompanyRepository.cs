using TrippleD.Core;
using TrippleD.Domain.Companies.Model;
using TrippleD.Domain.SharedKernel.EventDispatcher;

namespace TrippleD.Persistence.Repository
{
    [Service(typeof(IEntityRepository<Company>))]
    public class CompanyRepository: EntityRepository<Company>
    {
        public CompanyRepository(InMemoryStore.InMemoryStore store, IDomainEventDispatcher dispatcher) : base(store, dispatcher)
        {
        }
    }
}
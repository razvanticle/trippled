using TrippleD.Core;
using TrippleD.Domain.Company.Model;
using TrippleD.Domain.SharedKernel.EventDispatcher;

namespace TrippleD.Persistence.Repository
{
    [Service(typeof(IEntityRepository<Company, int>))]
    public class CompanyRepository: EntityRepository<Company,int>
    {
        public CompanyRepository(InMemoryStore.InMemoryStore store, IDomainEventDispatcher dispatcher) : base(store, dispatcher)
        {
        }
    }
}
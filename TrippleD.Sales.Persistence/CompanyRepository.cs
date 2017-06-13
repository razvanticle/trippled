using TrippleD.Core;
using TrippleD.Persistence.InMemoryStore;
using TrippleD.Persistence.Repository;
using TrippleD.Sales.Domain.Companies.Model;
using TrippleD.Sales.Domain.Companies.WorkItems;
using TrippleD.SharedKernel.EventDispatcher;

namespace TrippleD.Sales.Persistence
{
    [Service(typeof(IEntityRepository<Company>))]
    public class CompanyRepository: EntityRepository<Company>
    {
        public CompanyRepository(TrippleD.Persistence.InMemoryStore.InMemoryStore store, IDomainEventDispatcher dispatcher) : base(store, dispatcher)
        {
        }
    }

    [Service(typeof(IEntityRepository<WorkItem>))]
    public class WorkItemReository:EntityRepository<WorkItem>
    {
        public WorkItemReository(InMemoryStore store, IDomainEventDispatcher dispatcher) : base(store, dispatcher)
        {
        }
    }
}
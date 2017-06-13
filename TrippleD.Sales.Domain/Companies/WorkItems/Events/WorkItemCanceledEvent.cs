using TrippleD.SharedKernel.Events;
using TrippleD.SharedKernel.Identities;

namespace TrippleD.Sales.Domain.Companies.WorkItems.Events
{
    public class WorkItemCanceledEvent : DomainEvent
    {
        public WorkItemCanceledEvent(IIdentity workItemId)
        {
            WorkItemId = workItemId;
        }

        public IIdentity WorkItemId { get; }
    }
}
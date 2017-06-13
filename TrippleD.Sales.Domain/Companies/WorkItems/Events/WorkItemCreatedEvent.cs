using TrippleD.SharedKernel.Events;

namespace TrippleD.Sales.Domain.Companies.WorkItems.Events
{
    public class WorkItemCreatedEvent : DomainEvent
    {
        public WorkItemCreatedEvent(WorkItem workItem)
        {
            WorkItem = workItem;
        }

        public WorkItem WorkItem { get; }
    }
}
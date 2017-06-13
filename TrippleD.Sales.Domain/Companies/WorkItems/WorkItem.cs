using TrippleD.Sales.Domain.Companies.WorkItems.Events;
using TrippleD.SharedKernel;
using TrippleD.SharedKernel.Identities;
using TrippleD.SharedKernel.Model;

namespace TrippleD.Sales.Domain.Companies.WorkItems
{
    public class WorkItem : AggregateRoot
    {
        public WorkItem(IIdentity id, IIdentity productId, IIdentity customerId, TimeInterval timeInterval) : base(id)
        {
            ProductId = productId;
            CustomerId = customerId;
            TimeInterval = timeInterval;
            Status = WorkItemStatus.Pending;

            AddEvent(new WorkItemCreatedEvent(this));
        }

        public IIdentity CustomerId { get; }

        public IIdentity ProductId { get; }

        public WorkItemStatus Status { get; private set; }

        public TimeInterval TimeInterval { get; }

        public void Cancel()
        {
            Status = WorkItemStatus.CanceledByCustomer;

            AddEvent(new WorkItemCanceledEvent(Id));
        }
    }
}
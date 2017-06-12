using TrippleD.SharedKernel.Events;
using TrippleD.SharedKernel.Identities;

namespace TrippleD.Sales.Domain.ProductRequests.Events
{
    public class RequestAprovedEvent : DomainEvent
    {
        public RequestAprovedEvent(IIdentity requestId)
        {
            RequestId = requestId;
        }

        public IIdentity RequestId { get; }
    }
}
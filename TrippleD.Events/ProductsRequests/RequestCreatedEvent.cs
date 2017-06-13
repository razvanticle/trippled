using TrippleD.SharedKernel.Events;
using TrippleD.SharedKernel.Identities;

namespace TrippleD.Events.ProductsRequests
{
    public class RequestCreatedEvent : DomainEvent
    {
        public IIdentity RequestId { get; }

        public RequestCreatedEvent(IIdentity requestId)
        {
            RequestId = requestId;
        }
    }
}
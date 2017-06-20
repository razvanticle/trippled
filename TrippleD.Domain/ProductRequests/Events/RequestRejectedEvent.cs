using TrippleD.SharedKernel.Events;
using TrippleD.SharedKernel.Identities;

namespace TrippleD.Domain.ProductRequests.Events
{
    public class RequestRejectedEvent : DomainEvent
    {
        public RequestRejectedEvent(IIdentity requestId)
        {
            RequestId = requestId;
        }

        public IIdentity RequestId { get; }
    }
}
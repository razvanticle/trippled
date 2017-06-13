using TrippleD.SharedKernel.Events;
using TrippleD.SharedKernel.Identities;

namespace TrippleD.Events.ProductsRequests
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
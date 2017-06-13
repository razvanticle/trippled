using TrippleD.SharedKernel.Events;
using TrippleD.SharedKernel.Identities;

namespace TrippleD.Events.ProductsRequests
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
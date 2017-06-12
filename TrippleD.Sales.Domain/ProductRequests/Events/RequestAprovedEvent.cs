using TrippleD.Domain.SharedKernel.Events;
using TrippleD.Domain.SharedKernel.Identities;

namespace TrippleD.Domain.ProductRequests.Events
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
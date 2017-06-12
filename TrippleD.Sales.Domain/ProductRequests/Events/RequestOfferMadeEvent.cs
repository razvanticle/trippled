using TrippleD.Domain.SharedKernel.Events;

namespace TrippleD.Domain.ProductRequests.Events
{
    public class RequestOfferMadeEvent : DomainEvent
    {
        public RequestOffer RequestOffer { get; }

        public RequestOfferMadeEvent(RequestOffer requestOffer)
        {
            RequestOffer = requestOffer;
        }
    }
}
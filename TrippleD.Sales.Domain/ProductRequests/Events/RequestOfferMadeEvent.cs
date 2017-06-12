using TrippleD.SharedKernel.Events;

namespace TrippleD.Sales.Domain.ProductRequests.Events
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
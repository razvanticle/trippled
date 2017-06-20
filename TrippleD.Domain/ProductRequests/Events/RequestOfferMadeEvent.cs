using TrippleD.SharedKernel.Events;

namespace TrippleD.Domain.ProductRequests.Events
{
    public class RequestOfferMadeEvent : DomainEvent
    {
        public int Price { get; }

        public RequestOfferMadeEvent(int price)
        {
            Price = price;
        }
    }
}
using TrippleD.SharedKernel.Events;

namespace TrippleD.Management.Domain.ProductRequests.Events
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
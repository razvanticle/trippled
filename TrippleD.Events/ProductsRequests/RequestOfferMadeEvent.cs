using TrippleD.SharedKernel.Events;

namespace TrippleD.Events.ProductsRequests
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
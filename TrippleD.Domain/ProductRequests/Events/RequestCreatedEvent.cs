using TrippleD.SharedKernel.Events;

namespace TrippleD.Sales.Domain.ProductRequests.Events
{
    public class RequestCreatedEvent : DomainEvent
    {
        public RequestCreatedEvent(ProductRequest productRequest)
        {
            ProductRequest = productRequest;
        }

        public ProductRequest ProductRequest { get; }
    }
}
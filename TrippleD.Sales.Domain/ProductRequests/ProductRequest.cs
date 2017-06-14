using TrippleD.Sales.Domain.ProductRequests.Events;
using TrippleD.SharedKernel;
using TrippleD.SharedKernel.Identities;
using TrippleD.SharedKernel.Model;
using TrippleD.SharedKernel.Model.ProductRequest;

namespace TrippleD.Sales.Domain.ProductRequests
{
    public class ProductRequest : AggregateRoot
    {
        public ProductRequest(IIdentity requestId, IIdentity productId, IIdentity customerId, IIdentity companyId,
            TimeInterval timeInterval) : base(requestId)
        {
            ProductId = productId;
            CustomerId = customerId;
            CompanyId = companyId;
            TimeInterval = timeInterval;
            Status = RequestStatus.Pending;

            AddEvent(new RequestCreatedEvent(this));
        }

        public IIdentity CompanyId { get; }

        public IIdentity CustomerId { get; }

        public IIdentity ProductId { get; }

        public RequestStatus Status { get; private set; }

        public TimeInterval TimeInterval { get; }

        public void Cancel()
        {
            Status = RequestStatus.Canceled;
            AddEvent(new RequestCanceledEvent(Id));
        }
    }
}
using System;
using TrippleD.Domain.ProductRequests.Events;
using TrippleD.SharedKernel;
using TrippleD.SharedKernel.Identities;
using TrippleD.SharedKernel.Model;
using TrippleD.SharedKernel.Model.ProductRequest;

namespace TrippleD.Domain.ProductRequests
{
    public class ProductRequest : AggregateRoot
    {
        private RequestOffer requestOffer;

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

        public RequestOffer RequestOffer
        {
            get
            {
                if (requestOffer == null)
                {
                    throw new Exception("The offer has not been made on this request yet");
                }

                return requestOffer;
            }
        }

        public RequestStatus Status { get; private set; }

        public TimeInterval TimeInterval { get; }


        public void Approve()
        {
            if (requestOffer == null)
            {
                throw new Exception("An offer must be made first");
            }

            Status = RequestStatus.Accepted;
            AddEvent(new RequestAprovedEvent(Id));
        }

        public void Cancel()
        {
            Status = RequestStatus.Canceled;
            AddEvent(new RequestCanceledEvent(Id));
        }

        public void MakeRequestOffer(RequestOffer offer)
        {
            requestOffer = offer;
            AddEvent(new RequestOfferMadeEvent(offer.Price));
        }

        public void Reject()
        {
            if (requestOffer == null)
            {
                throw new Exception("An offer must be made first");
            }

            Status = RequestStatus.Rejected;
            AddEvent(new RequestRejectedEvent(Id));
        }
    }
}
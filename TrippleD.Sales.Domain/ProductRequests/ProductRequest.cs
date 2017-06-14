using System;
using System.Collections.Generic;
using TrippleD.Sales.Domain.ProductRequests.Events;
using TrippleD.SharedKernel;
using TrippleD.SharedKernel.Identities;
using TrippleD.SharedKernel.Model;

namespace TrippleD.Sales.Domain.ProductRequests
{
    public class ProductRequest : AggregateRoot
    {
        private RequestOffer requestOffer;

        public ProductRequest(IIdentity requestId, IIdentity productId, IIdentity customerId, IIdentity companyId, TimeInterval timeInterval) : base(requestId)
        {
            ProductId = productId;
            CustomerId = customerId;
            CompanyId = companyId;
            TimeInterval = timeInterval;
            Status = RequestStatus.Pending;

            AddEvent(new RequestCreatedEvent(this));
        }

        public IIdentity CustomerId { get; }

        public IIdentity CompanyId { get; }

        public TimeInterval TimeInterval { get; }

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

        public void Approve()
        {
            if (requestOffer == null)
            {
                throw new Exception("An offer must be made first");
            }

            Status = RequestStatus.Accepted;
            AddEvent(new RequestAprovedEvent(Id));
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

    public class CompanySchedule:AggregateRoot
    {
        public IIdentity CompanyId { get; }

        public IList<ProductRequest> ProductRequests { get; }

        public CompanySchedule(IIdentity companyId, IList<ProductRequest> productRequests) : base(Identity.Create())
        {
            CompanyId = companyId;
            ProductRequests = productRequests;
        }


    }
}
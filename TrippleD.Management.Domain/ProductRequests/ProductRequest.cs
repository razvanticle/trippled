using System;
using TrippleD.Management.Domain.ProductRequests.Events;
using TrippleD.SharedKernel;
using TrippleD.SharedKernel.Identities;
using TrippleD.SharedKernel.Model.ProductRequest;

namespace TrippleD.Management.Domain.ProductRequests
{
    public class ProductRequest : AggregateRoot
    {
        private RequestOffer requestOffer;

        public ProductRequest(IIdentity requestId, RequestStatus status, RequestOffer requestOffer) : base(requestId)
        {
            Status = status;
            this.requestOffer = requestOffer;
        }

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
}
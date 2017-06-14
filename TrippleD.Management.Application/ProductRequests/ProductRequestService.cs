using System;
using TrippleD.Core;
using TrippleD.Management.Domain.ProductRequests;
using TrippleD.Persistence.Repository;
using TrippleD.SharedKernel.Extensions;
using TrippleD.SharedKernel.Identities;
using TrippleD.SharedKernel.Model.ProductRequest;

namespace TrippleD.Management.Application.ProductRequests
{
    public interface IProductRequestService
    {
        void ApproveRequest(Guid requestId);
        void MakeRequestOffer(Guid requestId, int price);
    }

    [Service(typeof(IProductRequestService))]
    public class ProductRequestService : IProductRequestService
    {
        private readonly IEntityRepository<ProductRequest> requestRepository;

        public ProductRequestService(IEntityRepository<ProductRequest> requestRepository)
        {
            this.requestRepository = requestRepository;
        }

        public void ApproveRequest(Guid requestId)
        {
            Guard.ArgNotEmpty(requestId, nameof(requestId));

            IIdentity requestIdentity = requestId.ToIdentity();
            ProductRequest productRequest = requestRepository.GetEntityById(requestIdentity);
            if (productRequest == null)
            {
                throw new Exception("Product request not found");
            }

            productRequest.Approve();

            requestRepository.Update(productRequest);
        }

        public void MakeRequestOffer(Guid requestId, int price)
        {
            Guard.ArgNotEmpty(requestId, nameof(requestId));
            Guard.ArgNotEmpty(price, nameof(price));

            IIdentity requestIdentity = requestId.ToIdentity();

            ProductRequest productRequest = requestRepository.GetEntityById(requestIdentity);
            if (productRequest == null)
            {
                throw new Exception("Product request not found");
            }

            RequestOffer requestOffer = new RequestOffer(price);
            productRequest.MakeRequestOffer(requestOffer);

            requestRepository.Update(productRequest);
        }
    }
}
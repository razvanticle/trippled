using System;
using System.Collections.Generic;
using System.Linq;
using TrippleD.Application.ProductRequests.Dtos;
using TrippleD.Core;
using TrippleD.Domain.ProductRequests;
using TrippleD.Domain.ProductRequests.Specifications;
using TrippleD.Persistence.Repository;
using TrippleD.SharedKernel;
using TrippleD.SharedKernel.Extensions;
using TrippleD.SharedKernel.Identities;
using TrippleD.SharedKernel.Model;
using TrippleD.SharedKernel.Model.ProductRequest;
using TrippleD.SharedKernel.Specifications;

namespace TrippleD.Application.ProductRequests
{
    public interface IProductRequestService
    {
        void CancelRequest(Guid requestId);
        void RequestProduct(ProductRequestDto productRequestDto);
        void ApproveRequest(Guid requestId);
        void MakeRequestOffer(Guid requestId, RequestOfferDto requestOfferDto);
    }

    [Service(typeof(IProductRequestService))]
    public class ProductRequestService : IProductRequestService
    {
        private readonly IEntityRepository<ProductRequest> requestRepository;

        public ProductRequestService(IEntityRepository<ProductRequest> requestRepository)
        {
            this.requestRepository = requestRepository;
        }

        public void CancelRequest(Guid requestId)
        {
            Guard.ArgNotEmpty(requestId, nameof(requestId));

            IIdentity requestIdentity = requestId.ToIdentity();
            ProductRequest productRequest = requestRepository.GetEntityById(requestIdentity);
            if (productRequest == null)
            {
                throw new Exception("Product request not found");
            }

            productRequest.Cancel();

            requestRepository.Update(productRequest);
        }

        public void RequestProduct(ProductRequestDto productRequestDto)
        {
            Guard.ArgNotNull(productRequestDto,nameof(productRequestDto));
            
            IIdentity productIdentity = productRequestDto.ProductId.ToIdentity();
            IIdentity customerIdentity = productRequestDto.CustomerId.ToIdentity();
            IIdentity companyIdentity = productRequestDto.CompanyId.ToIdentity();

            ISpecification<ProductRequest> requestByProductAndCustomer =
                new ProductRequestByProductSpecification(productIdentity)
                    .And(new ProductRequestByCustomerSpecification(customerIdentity))
                    .And(new ProductRequestByCompanySpecification(companyIdentity))
                    .And(new PendingRequestSpecification());

            ProductRequest existingProductRequest = requestRepository.GetEntity(requestByProductAndCustomer);
            if (existingProductRequest != null)
            {
                throw new Exception("Request already exists");
            }

            TimeInterval timeInterval = new TimeInterval(productRequestDto.StartDate, productRequestDto.EndDate);
            ISpecification<ProductRequest> overlapingRequestSpecification =
                new ProductRequestOverlapsTimeIntervalSpecification(timeInterval)
                    .And(new ProductRequestByCompanySpecification(companyIdentity))
                    .And(new ProductRequestByProductSpecification(productIdentity));
            IEnumerable<ProductRequest> overlapingRequests =
                requestRepository.GetEntities(overlapingRequestSpecification)
                    .ToList();
            if (overlapingRequests.Any())
            {
                throw new Exception($"Request overlaps with other {overlapingRequests.Count()} requests");
            }

            ProductRequest productRequest = new ProductRequest(Identity.Create(Constants.ProductRequestsIds.Request1),
                productIdentity, customerIdentity, companyIdentity, timeInterval);

            requestRepository.Add(productRequest);
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

        public void MakeRequestOffer(Guid requestId, RequestOfferDto requestOfferDto)
        {
            Guard.ArgNotEmpty(requestId, nameof(requestId));
            Guard.ArgNotEmpty(requestOfferDto, nameof(requestOfferDto));

            IIdentity requestIdentity = requestId.ToIdentity();

            ProductRequest productRequest = requestRepository.GetEntityById(requestIdentity);
            if (productRequest == null)
            {
                throw new Exception("Product request not found");
            }

            RequestOffer requestOffer = new RequestOffer(requestOfferDto.Price);
            productRequest.MakeRequestOffer(requestOffer);

            requestRepository.Update(productRequest);
        }
    }
}
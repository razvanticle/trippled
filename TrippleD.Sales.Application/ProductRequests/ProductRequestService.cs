using System;
using System.Collections.Generic;
using System.Linq;
using TrippleD.Core;
using TrippleD.Persistence.Repository;
using TrippleD.Sales.Application.Extensions;
using TrippleD.Sales.Domain.Companies.Model;
using TrippleD.Sales.Domain.Customers.Model;
using TrippleD.Sales.Domain.ProductRequests;
using TrippleD.Sales.Domain.ProductRequests.Specifications;
using TrippleD.Sales.Domain.Products;
using TrippleD.SharedKernel;
using TrippleD.SharedKernel.Identities;
using TrippleD.SharedKernel.Model;
using TrippleD.SharedKernel.Specifications;

namespace TrippleD.Sales.Application.ProductRequests
{
    public interface IProductRequestService
    {
        void ApproveRequest(Guid requestId);
        void MakeRequestOffer(Guid requestId, int price);
        void RequestProduct(Guid productId, Guid customerId, Guid companyId, DateTime startDate, DateTime endDate);
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

        public void RequestProduct(Guid productId, Guid customerId, Guid companyId, DateTime startDate,
            DateTime endDate)
        {
            Guard.ArgNotEmpty(productId, nameof(productId));
            Guard.ArgNotEmpty(customerId, nameof(customerId));

            IIdentity productIdentity = productId.ToIdentity();
            IIdentity customerIdentity = customerId.ToIdentity();
            IIdentity companyIdentity = companyId.ToIdentity();

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

            TimeInterval timeInterval = new TimeInterval(startDate, endDate);
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
    }
}
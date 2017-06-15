using System;
using System.Collections.Generic;
using System.Linq;
using TrippleD.Core;
using TrippleD.Persistence.Repository;
using TrippleD.Sales.Application.ProductRequests.Dtos;
using TrippleD.Sales.Domain.ProductRequests;
using TrippleD.Sales.Domain.ProductRequests.Specifications;
using TrippleD.SharedKernel;
using TrippleD.SharedKernel.Extensions;
using TrippleD.SharedKernel.Identities;
using TrippleD.SharedKernel.Model;
using TrippleD.SharedKernel.Specifications;

namespace TrippleD.Sales.Application.ProductRequests
{
    public interface IProductRequestService
    {
        void CancelRequest(Guid requestId);
        void RequestProduct(ProductRequestDto productRequestDto);
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
    }
}
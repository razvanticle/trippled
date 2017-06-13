using System;
using TrippleD.Core;
using TrippleD.Persistence.Repository;
using TrippleD.Sales.Application.Extensions;
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
        void MakeRequestOffer(Guid requestId, int price);
        void RequestProduct(Guid productId, Guid customerId, DateTime startDate, DateTime endDate);
        void ApproveRequest(Guid requestId);
    }

    [Service(typeof(IProductRequestService))]
    public class ProductRequestService : IProductRequestService
    {
        private readonly IEntityRepository<Customer> customerRepository;
        private readonly IEntityRepository<Product> productRepository;
        private readonly IEntityRepository<ProductRequest> requestRepository;

        public ProductRequestService(IEntityRepository<ProductRequest> requestRepository,
            IEntityRepository<Product> productRepository, IEntityRepository<Customer> customerRepository)
        {
            this.requestRepository = requestRepository;
            this.productRepository = productRepository;
            this.customerRepository = customerRepository;
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

        public void RequestProduct(Guid productId, Guid customerId, DateTime startDate, DateTime endDate)
        {
            Guard.ArgNotEmpty(productId, nameof(productId));
            Guard.ArgNotEmpty(customerId, nameof(customerId));

            IIdentity productIdentity = productId.ToIdentity();
            IIdentity customerIdentity = customerId.ToIdentity();

            Product product = productRepository.GetEntityById(productIdentity);
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            Customer customer = customerRepository.GetEntityById(customerIdentity);
            if (customer == null)
            {
                throw new Exception("Customer not found");
            }

            ISpecification<ProductRequest> requestByProductAndCustomer =
                new ProductRequestByProductSpecification(productIdentity).And(
                    new ProductRequestByCustomerSpecification(customerIdentity)).And(new PendingRequestSpecification());
            ProductRequest existingProductRequest = requestRepository.GetEntity(requestByProductAndCustomer);
            if (existingProductRequest != null)
            {
                throw new Exception("Request already exists");
            }
            
            ProductRequest productRequest = new ProductRequest(Identity.Create(Constants.ProductRequestsIds.Request1),
                productIdentity, customerIdentity, new TimeInterval(startDate, endDate));

            requestRepository.Add(productRequest);
        }
    }
}
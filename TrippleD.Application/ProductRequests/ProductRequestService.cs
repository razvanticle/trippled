﻿using System;
using TrippleD.Core;
using TrippleD.Domain.Customers.Model;
using TrippleD.Domain.ProductRequests;
using TrippleD.Domain.ProductRequests.Specifications;
using TrippleD.Domain.Products;
using TrippleD.Domain.SharedKernel.Identities;
using TrippleD.Domain.SharedKernel.Specifications;
using TrippleD.Persistence.InMemoryStore;
using TrippleD.Persistence.Repository;

namespace TrippleD.Application.ProductRequests
{
    public interface IProductRequestService
    {
        void MakeRequestOffer(Guid requestId, int price);
        void RequestProduct(Guid productId, Guid customerId);
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

            IIdentity requestIdentity = Identity.Create(requestId);
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

            IIdentity requestIdentity = Identity.Create(requestId);

            ProductRequest productRequest = requestRepository.GetEntityById(requestIdentity);
            if (productRequest == null)
            {
                throw new Exception("Product request not found");
            }

            RequestOffer requestOffer = new RequestOffer(price);
            productRequest.MakeRequestOffer(requestOffer);

            requestRepository.Update(productRequest);
        }

        public void RequestProduct(Guid productId, Guid customerId)
        {
            Guard.ArgNotEmpty(productId, nameof(productId));
            Guard.ArgNotEmpty(customerId, nameof(customerId));

            IIdentity productIdentity = Identity.Create(productId);
            IIdentity customerIdentity = Identity.Create(customerId);

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
                productIdentity, customerIdentity);

            requestRepository.Add(productRequest);
        }
    }
}
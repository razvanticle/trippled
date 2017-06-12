using System;
using System.Linq.Expressions;
using TrippleD.SharedKernel.Identities;
using TrippleD.SharedKernel.Specifications;

namespace TrippleD.Sales.Domain.ProductRequests.Specifications
{
    public class ProductRequestByCustomerSpecification : Specification<ProductRequest>
    {
        private readonly IIdentity customerId;

        public ProductRequestByCustomerSpecification(IIdentity customerId)
        {
            this.customerId = customerId;
        }

        public override Expression<Func<ProductRequest, bool>> SpecExpression => x => x.CustomerId.Equals(customerId);
    }
}
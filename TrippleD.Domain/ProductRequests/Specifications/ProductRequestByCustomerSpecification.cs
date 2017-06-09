using System;
using System.Linq.Expressions;
using TrippleD.Domain.SharedKernel.Identities;
using TrippleD.Domain.SharedKernel.Specifications;

namespace TrippleD.Domain.ProductRequests.Specifications
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
using System;
using System.Linq.Expressions;
using TrippleD.SharedKernel.Identities;
using TrippleD.SharedKernel.Specifications;

namespace TrippleD.Sales.Domain.ProductRequests.Specifications
{
    public class ProductRequestByProductSpecification : Specification<ProductRequest>
    {
        private readonly IIdentity productId;

        public ProductRequestByProductSpecification(IIdentity productId)
        {
            this.productId = productId;
        }

        public override Expression<Func<ProductRequest, bool>> SpecExpression => x => x.ProductId.Equals(productId);
    }
}
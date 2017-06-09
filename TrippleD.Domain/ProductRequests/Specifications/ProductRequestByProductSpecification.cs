using System;
using System.Linq.Expressions;
using TrippleD.Domain.SharedKernel.Identities;
using TrippleD.Domain.SharedKernel.Specifications;

namespace TrippleD.Domain.ProductRequests.Specifications
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

    public class PendingRequestSpecification:Specification<ProductRequest>
    {
        public override Expression<Func<ProductRequest, bool>> SpecExpression => x => x.Status == RequestStatus.Pending;
    }
}
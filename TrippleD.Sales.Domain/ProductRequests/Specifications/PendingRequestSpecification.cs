using System;
using System.Linq.Expressions;
using TrippleD.SharedKernel.Specifications;

namespace TrippleD.Sales.Domain.ProductRequests.Specifications
{
    public class PendingRequestSpecification:Specification<ProductRequest>
    {
        public override Expression<Func<ProductRequest, bool>> SpecExpression => x => x.Status == RequestStatus.Pending;
    }
}
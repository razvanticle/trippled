using System;
using System.Linq.Expressions;
using TrippleD.SharedKernel.Model;
using TrippleD.SharedKernel.Specifications;

namespace TrippleD.Sales.Domain.ProductRequests.Specifications
{
    public class ProductRequestOverlapsTimeIntervalSpecification : Specification<ProductRequest>
    {
        private readonly TimeInterval timeInterval;

        public ProductRequestOverlapsTimeIntervalSpecification(TimeInterval timeInterval)
        {
            this.timeInterval = timeInterval;
        }

        public override Expression<Func<ProductRequest, bool>> SpecExpression =>
            x => x.TimeInterval.Overlaps(timeInterval);
    }
}
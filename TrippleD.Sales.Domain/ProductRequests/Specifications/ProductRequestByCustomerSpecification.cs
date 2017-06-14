using System;
using System.Linq.Expressions;
using TrippleD.SharedKernel.Identities;
using TrippleD.SharedKernel.Model;
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

    public class ProductRequestByCompanySpecification : Specification<ProductRequest>
    {
        private readonly IIdentity companyId;

        public ProductRequestByCompanySpecification(IIdentity companyId)
        {
            this.companyId = companyId;
        }

        public override Expression<Func<ProductRequest, bool>> SpecExpression => x => x.CompanyId.Equals(companyId);
    }

    public class ProductRequestOverlapSpecification:Specification<ProductRequest>
    {
        public TimeInterval TimeInterval { get; }

        public ProductRequestOverlapSpecification(TimeInterval timeInterval)
        {
            TimeInterval = timeInterval;
        }

        public override Expression<Func<ProductRequest, bool>> SpecExpression =>
            x => x.TimeInterval.Overlaps(TimeInterval);
    }
}
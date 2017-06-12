using TrippleD.Domain.SharedKernel.Identities;
using TrippleD.Domain.SharedKernel.Specifications;

namespace TrippleD.Domain.ProductRequests.Specifications
{
    public class ProductRequestByIdSpecification : EntityByIdSecification<ProductRequest>
    {
        public ProductRequestByIdSpecification(IIdentity id) : base(id)
        {
        }
    }
}
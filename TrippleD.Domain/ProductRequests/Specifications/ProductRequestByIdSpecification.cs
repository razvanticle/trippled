using TrippleD.SharedKernel.Identities;
using TrippleD.SharedKernel.Specifications;

namespace TrippleD.Domain.ProductRequests.Specifications
{
    public class ProductRequestByIdSpecification : EntityByIdSecification<ProductRequest>
    {
        public ProductRequestByIdSpecification(IIdentity id) : base(id)
        {
        }
    }
}
using TrippleD.Domain.SharedKernel.Identities;
using TrippleD.Domain.SharedKernel.Specifications;

namespace TrippleD.Domain.Products.Specifications
{
    public class ProductByIdSpecification : EntityByIdSecification<Product>
    {
        public ProductByIdSpecification(IIdentity id) : base(id)
        {
        }
    }
}
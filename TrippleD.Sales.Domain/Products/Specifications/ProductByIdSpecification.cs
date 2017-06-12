using TrippleD.SharedKernel.Identities;
using TrippleD.SharedKernel.Specifications;

namespace TrippleD.Sales.Domain.Products.Specifications
{
    public class ProductByIdSpecification : EntityByIdSecification<Product>
    {
        public ProductByIdSpecification(IIdentity id) : base(id)
        {
        }
    }
}
using TrippleD.Domain.Customers.Model;
using TrippleD.Domain.SharedKernel.Identities;
using TrippleD.Domain.SharedKernel.Specifications;

namespace TrippleD.Domain.Customers.Specifications
{
    public class CustomerByIdSpecification : EntityByIdSecification<Customer>
    {
        public CustomerByIdSpecification(IIdentity id) : base(id)
        {
        }
    }
}
using TrippleD.Domain.Customers.Model;
using TrippleD.SharedKernel.Identities;
using TrippleD.SharedKernel.Specifications;

namespace TrippleD.Domain.Customers.Specifications
{
    public class CustomerByIdSpecification : EntityByIdSecification<Customer>
    {
        public CustomerByIdSpecification(IIdentity id) : base(id)
        {
        }
    }
}
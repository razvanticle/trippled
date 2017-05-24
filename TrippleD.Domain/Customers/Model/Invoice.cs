using TrippleD.Domain.SharedKernel;
using TrippleD.Domain.SharedKernel.Model;

namespace TrippleD.Domain.Customers.Model
{
    public class Invoice : ValueObjectBase<Invoice>
    {
        public Invoice(Address address)
        {
            Address = address;
        }

        public Address Address { get; }
    }
}
using TrippleD.SharedKernel.Dtos;

namespace TrippleD.Customers.Dtos
{
    public class OrderItemDto
    {
        public AddressDto ExecutionAddress { get; set; }

        public ServiceDto Service { get; set; }
    }
}
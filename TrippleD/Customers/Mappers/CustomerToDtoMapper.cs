using System.Linq;
using TrippleD.Core;
using TrippleD.Core.Mappers;
using TrippleD.Customers.Dtos;
using TrippleD.Domain.Customers.Model;

namespace TrippleD.Customers.Mappers
{
    public class CustomerToDtoMapper : IMapper<Customer, CustomerDto>
    {
        private readonly IMapper mapper;

        public CustomerToDtoMapper(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public CustomerDto Map(Customer customer)
        {
            Guard.ArgNotNull(customer, nameof(customer));

            return new CustomerDto
            {
                Id = customer.Id.Value,
                Name = customer.Name.ToString(),
                Email = customer.Email,
                MobilePhone = customer.MobilePhone,
                Orders = customer.Orders.Select(mapper.Map<Order, OrderDto>)
            };
        }
    }
}
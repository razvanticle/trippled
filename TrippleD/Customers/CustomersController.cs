using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TrippleD.Core.Extensions;
using TrippleD.Core.Mappers;
using TrippleD.Customers.Dtos;
using TrippleD.Domain.Customers.Model;
using TrippleD.Domain.SharedKernel;
using TrippleD.Persistence.Repository;

namespace TrippleD.Customers
{
    [Route("api/customers")]
    public class CustomersController : Controller
    {
        private readonly IEntityRepository<Customer, int> customerRepository;
        private readonly IMapper mapper;

        public CustomersController(IEntityRepository<Customer, int> customerRepository, IMapper mapper)
        {
            this.customerRepository = customerRepository;
            this.mapper = mapper;
        }

        [HttpPost("{customerId}/orders")]
        public IActionResult PostOrder(int customerId, [FromBody]OrderDto orderDto)
        {
            var customer = customerRepository.GetEntities( /*by id*/).FirstOrDefault();
            var order = orderDto.Execute(mapper.Map<OrderDto, Order>);
            customer.AddOrder(order);

            return Ok();
        }

        [HttpGet]
        public IActionResult GetCustomers()
        {
            var customers = customerRepository.GetEntities()
                .Select(mapper.Map<Customer, CustomerDto>);

            return Ok(customers);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TrippleD.Core.Extensions;
using TrippleD.Core.Mappers;
using TrippleD.Customers.Dtos;
using TrippleD.Domain.Customers.Model;
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

        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            ISpecification<Customer> specification = new CustomerIdSpecification(id).And(new CustomerEmailSpecification("jdoe@gmail.com"));
            Customer customer = customerRepository.GetEntity(specification);
            if (customer == null)
            {
                return NotFound($"Customer with id {id} was not found");
            }

            CustomerDto customerDto = customer.Execute(mapper.Map<Customer, CustomerDto>);
            return Ok(customerDto);
        }

        [HttpGet]
        public IActionResult GetCustomers()
        {
            IEnumerable<CustomerDto> customers = customerRepository.GetEntities()
                .Select(mapper.Map<Customer, CustomerDto>);

            return Ok(customers);
        }

        [HttpPost("{customerId}/orders")]
        public IActionResult PostOrder(int customerId, [FromBody] OrderDto orderDto)
        {
            ISpecification<Customer> specification = new CustomerIdSpecification(customerId);

            Customer customer = customerRepository.GetEntity(specification);
            Order order = orderDto.Execute(mapper.Map<OrderDto, Order>);
            customer.AddOrder(order);

            return Ok();
        }
    }
}
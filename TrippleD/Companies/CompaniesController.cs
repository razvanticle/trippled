using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TrippleD.Companies.Dtos;
using TrippleD.Core.Extensions;
using TrippleD.Core.Mappers;
using TrippleD.Domain.Company.Model;
using TrippleD.Persistence.Repository;

namespace TrippleD.Companies
{
    [Route("api/companies")]
    public class CompaniesController : Controller
    {
        private readonly IMapper mapper;
        private readonly IEntityRepository<Company, int> repository;

        public CompaniesController(IEntityRepository<Company, int> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var companies = repository.GetEntities()
                .ToList();

            return Ok(companies);
        }

        [HttpGet("{id}/services")]
        public IActionResult GetCompanyServices(int id)
        {
            var company = repository.GetEntities().FirstOrDefault(x => x.Id == id);
            if (company == null)
            {
                return NotFound($"Company with id {id} was not found");
            }

            var services = company.Services.Select(mapper.Map<Service, ServiceDto>);

            return Ok(services);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] int value)
        {
            var company = repository.GetEntities()
                .FirstOrDefault(x => x.Id == id);

            company.RateCompany(value);
            company.RemoveService("service 2");
            repository.Update(company);
            
            return Ok();
        }
    }
}
using System.Collections.Generic;
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
            List<CompanyDto> companies = repository.GetEntities()
                .Select(mapper.Map<Company, CompanyDto>)
                .ToList();

            return Ok(companies);
        }

        [HttpGet("{id}")]
        public IActionResult GetCompany(int id)
        {
            Company company = repository.GetEntities().FirstOrDefault(x => x.Id == id);
            if (company == null)
            {
                return NotFound($"Company with id {id} was not found");
            }

            CompanyDto companyDto = company.Execute(mapper.Map<Company, CompanyDto>);

            return Ok(companyDto);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] int value)
        {
            Company company = repository.GetEntities()
                .FirstOrDefault(x => x.Id == id);

            company.RateCompany(value);
            company.RemoveService("service 2");
            repository.Update(company);

            return Ok();
        }
    }
}
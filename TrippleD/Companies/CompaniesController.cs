using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TrippleD.Companies.Dtos;
using TrippleD.Core.Extensions;
using TrippleD.Core.Mappers;
using TrippleD.Domain.Companies.Model;
using TrippleD.Persistence.Repository;
using TrippleD.SharedKernel.Identities;

namespace TrippleD.Companies
{
    [Route("api/companies")]
    public class CompaniesController : Controller
    {
        private readonly IMapper mapper;
        private readonly IEntityRepository<Company> repository;

        public CompaniesController(IEntityRepository<Company> repository, IMapper mapper)
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
        public IActionResult GetCompany(Guid id)
        {
            Company company = repository.GetEntities().FirstOrDefault(x => Equals(x.Id, Identity.Create(id)));
            if (company == null)
            {
                return NotFound($"Company with id {id} was not found");
            }

            CompanyDto companyDto = company.Execute(mapper.Map<Company, CompanyDto>);

            return Ok(companyDto);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] int value)
        {
            Company company = repository.GetEntities()
                .FirstOrDefault(x => Equals(x.Id, Identity.Create(id)));

            company.RateCompany(value);
            company.RemoveService("service 2");
            repository.Update(company);

            return Ok();
        }
    }
}
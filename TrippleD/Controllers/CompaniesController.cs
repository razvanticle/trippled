using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TrippleD.Domain.Company.Repositories;
using TrippleD.Persistence.Model;
using TrippleD.Persistence.Repository;

namespace TrippleD.Controllers
{
    [Route("api/[controller]")]
    public class CompaniesController : Controller
    {
        private readonly ICompanyRepository companyRepository;

        public CompaniesController(ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            var companies = companyRepository.GetCompanies()
                .ToList();

            return Ok(companies);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] int value)
        {
            var company = companyRepository.GetCompanies().FirstOrDefault(x => x.Id == id);
            company.RateCompany(value);

            companyRepository.UpdateCompany(company);

            return Ok();
        }
    }
}
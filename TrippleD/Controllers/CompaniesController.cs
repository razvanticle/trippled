using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrippleD.Core.Extensions;
using TrippleD.Domain.Company.Model;
using TrippleD.Persistence.Repository;

namespace TrippleD.Controllers
{
    [Route("api/[controller]")]
    public class CompaniesController : Controller
    {
        private readonly IRepository repository;

        public CompaniesController(IRepository repository)
        {
            this.repository = repository;
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
            var companies = repository.GetEntities<Company>()
                .Include(x=>x.Services)
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
            Disposable.Using(repository.CreateUnitOfWork,
                uow =>
                {
                    var company = uow.GetEntities<Company>()
                        .Include(x => x.Services)
                        .FirstOrDefault(x => x.Id == id);
                    company.RateCompany(value);
                    company.RemoveService("service 2");

                    uow.SaveChanges();
                });

            return Ok();
        }
    }
}
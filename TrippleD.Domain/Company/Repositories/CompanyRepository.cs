using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TrippleD.Domain.Company.Model;
using TrippleD.Domain.SharedKernel;
using TrippleD.Persistence.Repository;

namespace TrippleD.Domain.Company.Repositories
{
    public interface IEntityRepository<TEntity, TKey> where TEntity : Entity<TKey>
    {
    }

    public interface ICompanyRepository : IEntityRepository<Model.Company, int>
    {
        IEnumerable<Model.Company> GetCompanies();
        void UpdateCompany(Model.Company company);
    }

    public class CompanyRepository : ICompanyRepository
    {
        private readonly IRepository repository;

        public CompanyRepository(IRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Model.Company> GetCompanies()
        {
            var companies = repository.GetEntities<Persistence.Model.Company>().Select(x => new Model.Company(x.Id)
            {
                Rating = x.Rating,
                Email = x.Email,
                Name = x.Name,
                PhoneNumber = x.PhoneNumber,
                WebSite = x.WebSite,
                Address = new Address(x.Address.City, x.Address.Number, x.Address.Street),
                BusinessHours = new TimeInterval(new Time(x.OpenTime.Hour, x.OpenTime.Minute),
                    new Time(x.CloseTime.Hour, x.CloseTime.Minute)),
                Services = x.Services.Select(s => new Service(s.Name, s.Rating))
            });

            return companies;
        }

        public void UpdateCompany(Model.Company company)
        {
            var dbCompany = repository.GetEntities<Persistence.Model.Company>()
                .Include(x => x.Address)
                .FirstOrDefault(x => x.Id == company.Id);

            if (dbCompany == null)
            {
                throw new Exception("Company not found");
            }

            dbCompany.Name = company.Name;
            dbCompany.Email = company.Email;
            dbCompany.Rating = company.Rating;

            dbCompany.Address.City = company.Address.City;
            dbCompany.Address.Number = company.Address.Number;
            dbCompany.Address.Street = company.Address.Street;

            repository.Update(dbCompany);
            repository.SaveChanges();
        }
    }
}
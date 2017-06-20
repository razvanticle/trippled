using System.Linq;
using TrippleD.Companies.Dtos;
using TrippleD.Core;
using TrippleD.Core.Extensions;
using TrippleD.Core.Mappers;
using TrippleD.Domain.Companies.Model;
using TrippleD.SharedKernel.Dtos;
using TrippleD.SharedKernel.Model;

namespace TrippleD.Companies.Mappers
{
    public class CompanyToDtoMapper : IMapper<Company, CompanyDto>
    {
        private readonly IMapper mapper;

        public CompanyToDtoMapper(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public CompanyDto Map(Company company)
        {
            Guard.ArgNotNull(company, nameof(company));

            return new CompanyDto
            {
                Id = company.Id.Value,
                Name = company.Name,
                Rating = company.Rating,
                Email = company.Email,
                WebSite = company.WebSite,
                PhoneNumber = company.PhoneNumber,
                Address = company.Address?.Execute(mapper.Map<Address, AddressDto>),
                BusinessHours = company.BusinessHours?.Execute(mapper.Map<TimeInterval, TimeIntervalDto>),
                Services = company.Services?.Select(mapper.Map<Service, ServiceDto>).ToList()
            };
        }
    }
}
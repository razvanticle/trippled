using TrippleD.Companies.Dtos;
using TrippleD.Core.Mappers;
using TrippleD.Domain.Company.Model;

namespace TrippleD.Companies.Mappers
{
    public class ServiceMapper : IMapper<Service, ServiceDto>
    {
        public ServiceDto Map(Service input)
        {
            return new ServiceDto
            {
                Name = input.Name,
                Rating = input.Rating
            };
        }
    }
}
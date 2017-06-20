using TrippleD.Core;
using TrippleD.Core.Mappers;
using TrippleD.Domain.Companies.Model;
using TrippleD.SharedKernel.Dtos;

namespace TrippleD.SharedKernel.Mappers
{
    public class ServiceToDtoMapper : IMapper<Service, ServiceDto>, IMapper<ServiceDto,Service>
    {
        public ServiceDto Map(Service service)
        {
            Guard.ArgNotNull(service, nameof(service));

            return new ServiceDto
            {
                Name = service.Name,
                Rating = service.Rating
            };
        }

        public Service Map(ServiceDto serviceDto)
        {
            Guard.ArgNotNull(serviceDto,nameof(serviceDto));

            return new Service(serviceDto.Name, serviceDto.Rating);
        }
    }
}
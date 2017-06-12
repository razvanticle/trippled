using TrippleD.Core;
using TrippleD.Core.Mappers;
using TrippleD.SharedKernel.Dtos;
using TrippleD.SharedKernel.Model;

namespace TrippleD.SharedKernel.Mappers
{
    public class TimeToDtoMapper : IMapper<Time, TimeDto>
    {
        public TimeDto Map(Time time)
        {
            Guard.ArgNotNull(time, nameof(time));

            return new TimeDto
            {
                Hour = time.Hour,
                Minute = time.Minute
            };
        }
    }
}
using TrippleD.Core;
using TrippleD.Core.Extensions;
using TrippleD.Core.Mappers;
using TrippleD.SharedKernel.Dtos;
using TrippleD.SharedKernel.Model;

namespace TrippleD.SharedKernel.Mappers
{
    public class TimeIntervalToDtoMapper : IMapper<TimeInterval, TimeIntervalDto>
    {
        private readonly IMapper mapper;

        public TimeIntervalToDtoMapper(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public TimeIntervalDto Map(TimeInterval timeInterval)
        {
            Guard.ArgNotNull(timeInterval, nameof(timeInterval));

            return new TimeIntervalDto
            {
                EndTime = timeInterval.EndTime.Execute<Time, TimeDto>(mapper.Map<Time, TimeDto>),
                StartTime = timeInterval.StartTime.Execute<Time, TimeDto>(mapper.Map<Time, TimeDto>)
            };
        }
    }
}
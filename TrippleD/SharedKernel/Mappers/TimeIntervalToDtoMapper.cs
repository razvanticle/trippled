using System;
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
                EndTime = timeInterval.End.Execute(mapper.Map<DateTime, TimeDto>),
                StartTime = timeInterval.Start.Execute(mapper.Map<DateTime, TimeDto>)
            };
        }
    }
}
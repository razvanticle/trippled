using System;
using TrippleD.Core;
using TrippleD.Core.Mappers;
using TrippleD.SharedKernel.Dtos;

namespace TrippleD.SharedKernel.Mappers
{
    public class TimeToDtoMapper : IMapper<DateTime, TimeDto>
    {
        public TimeDto Map(DateTime time)
        {
            Guard.ArgNotEmpty(time, nameof(time));

            return new TimeDto
            {
                Hour = time.Hour,
                Minute = time.Minute
            };
        }
    }
}
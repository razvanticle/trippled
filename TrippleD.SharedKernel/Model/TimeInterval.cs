using System;

namespace TrippleD.SharedKernel.Model
{
    public class TimeInterval : ValueObjectBase<TimeInterval>
    {
        public TimeInterval(DateTime start, DateTime end)
        {
            if (start > end)
            {
                throw new Exception("Start date cannot be greather than end date");
            }

            Start = start;
            End = end;
        }

        public DateTime End { get; }
        public DateTime Start { get; }

        public bool Overlaps(TimeInterval dateTimeRange)
        {
            return Start < dateTimeRange.End &&
                   End > dateTimeRange.Start;
        }
    }
}
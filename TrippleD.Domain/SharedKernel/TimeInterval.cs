namespace TrippleD.Domain.SharedKernel
{
    public class TimeInterval : ValueObjectBase<TimeInterval>
    {
        public TimeInterval(Time startTime, Time endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
        }

        public Time EndTime { get; }

        public Time StartTime { get; }
    }
}
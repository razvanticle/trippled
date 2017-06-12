using System;

namespace TrippleD.SharedKernel.Events
{
    public interface IDomainEvent
    {
        DateTime DateTimeEventOccurred { get; }

        Guid Id { get; }
    }
}
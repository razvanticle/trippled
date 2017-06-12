using System;

namespace TrippleD.Domain.SharedKernel.Events
{
    public interface IDomainEvent
    {
        DateTime DateTimeEventOccurred { get; }

        Guid Id { get; }
    }
}
using TrippleD.Domain.SharedKernel.Events;

namespace TrippleD.Domain.SharedKernel.EventDispatcher
{
    public interface IDomainEventDispatcher
    {
        void Dispatch(IDomainEvent domainEvent);
    }
}
using TrippleD.SharedKernel.Events;

namespace TrippleD.SharedKernel.EventDispatcher
{
    public interface IDomainEventDispatcher
    {
        void Dispatch(IDomainEvent domainEvent);
    }
}
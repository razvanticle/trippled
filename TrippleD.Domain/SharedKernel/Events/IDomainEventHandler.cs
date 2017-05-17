namespace TrippleD.Domain.SharedKernel.Events
{
    public interface IDomainEventHandler<T> where T : IDomainEvent
    {
        void Handle(T args);
    }
}
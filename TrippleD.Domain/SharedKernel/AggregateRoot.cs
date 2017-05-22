using System.Collections.Generic;
using TrippleD.Domain.SharedKernel.Events;

namespace TrippleD.Domain.SharedKernel
{
    public class AggregateRoot<TId> : Entity<TId>
    {
        private readonly IList<IDomainEvent> domainEvents;

        public AggregateRoot()
        {
            domainEvents = new List<IDomainEvent>();
        }

        public AggregateRoot(TId id) : base(id)
        {
            domainEvents = new List<IDomainEvent>();
        }

        public IEnumerable<IDomainEvent> DomainEvents => domainEvents;

        public void AddEvent(IDomainEvent domainEvent)
        {
            domainEvents.Add(domainEvent);
        }

        public void ClearEvents()
        {
            domainEvents.Clear();
        }
    }
}
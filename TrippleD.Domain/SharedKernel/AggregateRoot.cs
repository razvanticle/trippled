using System.Collections.Generic;
using TrippleD.Domain.SharedKernel.Events;
using TrippleD.Domain.SharedKernel.Identities;

namespace TrippleD.Domain.SharedKernel
{
    public class AggregateRoot : Entity
    {
        private readonly IList<IDomainEvent> domainEvents;
        
        public AggregateRoot(IIdentity id) : base(id)
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
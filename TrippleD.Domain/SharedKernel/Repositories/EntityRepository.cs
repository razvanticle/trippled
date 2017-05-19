using TrippleD.Core;
using TrippleD.Domain.SharedKernel.EventDispatcher;

namespace TrippleD.Domain.SharedKernel.Repositories
{
    public abstract class EntityRepository<TEntity, TKey> where TEntity : AggregateRoot<TKey>
    {
        private readonly IDomainEventDispatcher dispatcher;

        protected EntityRepository(IDomainEventDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        protected void DispatchEvents(TEntity entity)
        {
            Guard.ArgNotNull(entity, nameof(entity));

            foreach (var domainEvent in entity.DomainEvents)
            {
                dispatcher.Dispatch(domainEvent);
            }
        }
    }
}
using System.Collections.Generic;
using TrippleD.Core;
using TrippleD.Domain.SharedKernel;
using TrippleD.Domain.SharedKernel.EventDispatcher;

namespace TrippleD.Persistence.Repository
{
    public abstract class EntityRepository<TEntity, TKey> : IEntityRepository<TEntity, TKey>
        where TEntity : AggregateRoot<TKey>
    {
        private readonly InMemoryStore.InMemoryStore store;
        protected readonly IDomainEventDispatcher Dispatcher;

        protected EntityRepository(InMemoryStore.InMemoryStore store, IDomainEventDispatcher dispatcher)
        {
            this.store = store;
            Dispatcher = dispatcher;
        }

        public void Add(TEntity entity)
        {
            store.Add(entity);
            DispatchEvents(entity);
        }

        public void Update(TEntity entity)
        {
            DispatchEvents(entity);
        }

        public void Delete(TEntity entity)
        {
            store.Delete(entity);
            DispatchEvents(entity);
        }

        public IEnumerable<TEntity> GetEntities()
        {
            return GetEntities(null);
        }

        public IEnumerable<TEntity> GetEntities(SelectionCriteria<TEntity, TKey> criteria)
        {
            return store.GetEntities<TEntity>();
        }
        
        protected void DispatchEvents(TEntity entity)
        {
            Guard.ArgNotNull(entity, nameof(entity));

            foreach (var domainEvent in entity.DomainEvents)
            {
                Dispatcher.Dispatch(domainEvent);
            }
        }
    }
}
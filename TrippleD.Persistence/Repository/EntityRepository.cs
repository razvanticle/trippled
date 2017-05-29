using System.Collections.Generic;
using System.Linq;
using TrippleD.Core;
using TrippleD.Domain.SharedKernel;
using TrippleD.Domain.SharedKernel.EventDispatcher;
using TrippleD.Domain.SharedKernel.Events;
using TrippleD.Domain.SharedKernel.Specifications;

namespace TrippleD.Persistence.Repository
{
    public abstract class EntityRepository<TEntity, TKey> : IEntityRepository<TEntity, TKey>
        where TEntity : AggregateRoot<TKey>
    {
        protected readonly IDomainEventDispatcher Dispatcher;
        private readonly InMemoryStore.InMemoryStore store;

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

        public void Delete(TEntity entity)
        {
            store.Delete(entity);
            DispatchEvents(entity);
        }

        public IEnumerable<TEntity> GetEntities()
        {
            return store.GetEntities<TEntity>();
        }

        public IEnumerable<TEntity> GetEntities(ISpecification<TEntity> criteria)
        {
            return store.GetEntities<TEntity>().Where(criteria.IsSatisfiedBy);
        }

        public TEntity GetEntity(ISpecification<TEntity> specification)
        {
            return GetEntities(specification).FirstOrDefault();
        }

        public void Update(TEntity entity)
        {
            DispatchEvents(entity);
        }

        protected void DispatchEvents(TEntity entity)
        {
            Guard.ArgNotNull(entity, nameof(entity));

            foreach (IDomainEvent domainEvent in entity.DomainEvents)
            {
                Dispatcher.Dispatch(domainEvent);
            }
        }
    }
}
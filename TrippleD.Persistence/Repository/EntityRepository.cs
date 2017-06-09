using System.Collections.Generic;
using System.Linq;
using TrippleD.Core;
using TrippleD.Domain.SharedKernel;
using TrippleD.Domain.SharedKernel.EventDispatcher;
using TrippleD.Domain.SharedKernel.Events;
using TrippleD.Domain.SharedKernel.Identities;
using TrippleD.Domain.SharedKernel.Specifications;

namespace TrippleD.Persistence.Repository
{
    public abstract class EntityRepository<TAggregate> : IEntityRepository<TAggregate>
        where TAggregate : AggregateRoot
    {
        protected readonly IDomainEventDispatcher Dispatcher;
        private readonly InMemoryStore.InMemoryStore store;

        protected EntityRepository(InMemoryStore.InMemoryStore store, IDomainEventDispatcher dispatcher)
        {
            this.store = store;
            Dispatcher = dispatcher;
        }

        public void Add(TAggregate entity)
        {
            store.Add(entity);
            DispatchEvents(entity);
        }

        public void Delete(TAggregate entity)
        {
            store.Delete(entity);
            DispatchEvents(entity);
        }

        public IEnumerable<TAggregate> GetEntities()
        {
            return store.GetEntities<TAggregate>();
        }

        public IEnumerable<TAggregate> GetEntities(ISpecification<TAggregate> criteria)
        {
            return store.GetEntities<TAggregate>().Where(criteria.IsSatisfiedBy);
        }

        public TAggregate GetEntity(ISpecification<TAggregate> specification)
        {
            return GetEntities(specification).FirstOrDefault();
        }

        public TAggregate GetEntityById(IIdentity id)
        {
            ISpecification<TAggregate> entityByIdSpecification = new EntityByIdSecification<TAggregate>(id);

            return GetEntity(entityByIdSpecification);
        }

        public void Update(TAggregate entity)
        {
            DispatchEvents(entity);
        }

        protected void DispatchEvents(TAggregate entity)
        {
            Guard.ArgNotNull(entity, nameof(entity));

            foreach (IDomainEvent domainEvent in entity.DomainEvents)
            {
                Dispatcher.Dispatch(domainEvent);
            }
        }
    }
}
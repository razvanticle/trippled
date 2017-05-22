using System.Collections.Generic;
using TrippleD.Core;
using TrippleD.Domain.Company.Model;
using TrippleD.Domain.SharedKernel;
using TrippleD.Domain.SharedKernel.EventDispatcher;

namespace TrippleD.Persistence.Repository
{
    public abstract class EntityRepository<TEntity, TKey> : IEntityRepository<TEntity, TKey>
        where TEntity : AggregateRoot<TKey>
    {
        protected readonly IDomainEventDispatcher Dispatcher;

        private readonly IList<TEntity> entities = new List<TEntity>();

        protected EntityRepository(IDomainEventDispatcher dispatcher)
        {
            Dispatcher = dispatcher;
        }

        public void Add(TEntity entity)
        {
            entities.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            entities.Remove(entity);
        }

        public IEnumerable<TEntity> GetEntities(SelectionCriteria<TEntity, TKey> criteria)
        {
            return entities;
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

    public interface IEntityRepository<TEntity, TKey> where TEntity : AggregateRoot<TKey>
    {
        void Add(TEntity entity);

        void Delete(TEntity entity);

        IEnumerable<TEntity> GetEntities(SelectionCriteria<TEntity, TKey> criteria);
    }

    public class SelectionCriteria<TEntity, TKey> where TEntity : AggregateRoot<TKey>
    {
    }

    public class InMemoryStore
    {
        public IEnumerable<Company> Companies { get; set; }
    }
}
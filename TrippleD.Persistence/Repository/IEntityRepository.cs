using System.Collections.Generic;
using TrippleD.Domain.SharedKernel;

namespace TrippleD.Persistence.Repository
{
    public interface IEntityRepository<TEntity, TKey> where TEntity : AggregateRoot<TKey>
    {
        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        IEnumerable<TEntity> GetEntities();

        IEnumerable<TEntity> GetEntities(SelectionCriteria<TEntity, TKey> criteria);
    }
}
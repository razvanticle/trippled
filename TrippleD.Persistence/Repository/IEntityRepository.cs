using System.Collections.Generic;
using TrippleD.Domain.SharedKernel;
using TrippleD.Domain.SharedKernel.Specifications;

namespace TrippleD.Persistence.Repository
{
    public interface IEntityRepository<TEntity, in TKey> where TEntity : AggregateRoot<TKey>
    {
        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        TEntity GetEntity(ISpecification<TEntity> specification);

        IEnumerable<TEntity> GetEntities();

        IEnumerable<TEntity> GetEntities(ISpecification<TEntity> specification);
    }
}
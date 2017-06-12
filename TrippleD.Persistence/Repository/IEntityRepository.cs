using System.Collections.Generic;
using TrippleD.SharedKernel;
using TrippleD.SharedKernel.Identities;
using TrippleD.SharedKernel.Specifications;

namespace TrippleD.Persistence.Repository
{
    public interface IEntityRepository<TAggregate> where TAggregate : AggregateRoot
    {
        void Add(TAggregate entity);

        void Delete(TAggregate entity);

        IEnumerable<TAggregate> GetEntities();

        IEnumerable<TAggregate> GetEntities(ISpecification<TAggregate> specification);

        TAggregate GetEntity(ISpecification<TAggregate> specification);

        TAggregate GetEntityById(IIdentity id);

        void Update(TAggregate entity);
    }
}
using System;
using System.Linq.Expressions;
using TrippleD.SharedKernel.Identities;

namespace TrippleD.SharedKernel.Specifications
{
    public class EntityByIdSecification<T> : Specification<T> where T : Entity
    {
        private readonly IIdentity id;

        public EntityByIdSecification(IIdentity id)
        {
            this.id = id;
        }

        public override Expression<Func<T, bool>> SpecExpression => x => x.Id.Equals(id);
    }
}
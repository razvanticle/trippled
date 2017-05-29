using System;
using System.Linq.Expressions;

namespace TrippleD.Domain.SharedKernel.Specifications
{
    public abstract class Specification<T> : ISpecification<T>
    {
        private Func<T, bool> predicate;

        public abstract Expression<Func<T, bool>> SpecExpression { get; }

        public bool IsSatisfiedBy(T entity)
        {
            if (predicate == null)
            {
                predicate = SpecExpression.Compile();
            }

            return predicate(entity);
        }
    }
}
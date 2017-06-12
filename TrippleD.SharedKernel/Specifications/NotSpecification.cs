using System;
using System.Linq;
using System.Linq.Expressions;

namespace TrippleD.SharedKernel.Specifications
{
    public class NotSpecification<T> : Specification<T>
    {
        private readonly ISpecification<T> inner;

        public NotSpecification(ISpecification<T> inner)
        {
            this.inner = inner;
        }

        public override Expression<Func<T, bool>> SpecExpression => BuildNotExpression();

        private Expression<Func<T, bool>> BuildNotExpression()
        {
            Expression<Func<T, bool>> innerExpression = inner.SpecExpression;
            UnaryExpression notExression = Expression.Not(innerExpression.Body);

            return Expression.Lambda<Func<T, bool>>(notExression, innerExpression.Parameters.Single());
        }
    }
}
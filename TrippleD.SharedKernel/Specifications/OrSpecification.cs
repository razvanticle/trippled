using System;
using System.Linq.Expressions;

namespace TrippleD.Domain.SharedKernel.Specifications
{
    public class OrSpecification<T> : Specification<T>
    {
        private readonly ISpecification<T> left;
        private readonly ISpecification<T> right;

        public OrSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            this.left = left;
            this.right = right;
        }

        public override Expression<Func<T, bool>> SpecExpression => BuildOrExpression();

        private Expression<Func<T, bool>> BuildOrExpression()
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            InvocationExpression invocationExpressionLeft = Expression.Invoke(left.SpecExpression, parameter);
            InvocationExpression invocationExpressionRight = Expression.Invoke(right.SpecExpression, parameter);

            BinaryExpression orExpression = Expression.OrElse(invocationExpressionLeft, invocationExpressionRight);

            return Expression.Lambda<Func<T, bool>>(orExpression, parameter);
        }
    }
}
using System;
using System.Linq.Expressions;

namespace TrippleD.SharedKernel.Specifications
{
    public class AndSpecification<T> : Specification<T>
    {
        private readonly ISpecification<T> left;
        private readonly ISpecification<T> right;

        public AndSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            this.left = left;
            this.right = right;
        }

        public override Expression<Func<T, bool>> SpecExpression => BuildAndExpression();

        private Expression<Func<T, bool>> BuildAndExpression()
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            InvocationExpression invocationExpressionLeft = Expression.Invoke(left.SpecExpression, parameter);
            InvocationExpression invocationExpressionRight = Expression.Invoke(right.SpecExpression, parameter);

            BinaryExpression andAlsoExpression =
                Expression.AndAlso(invocationExpressionLeft, invocationExpressionRight);

            return Expression.Lambda<Func<T, bool>>(andAlsoExpression, parameter);
        }
    }
}
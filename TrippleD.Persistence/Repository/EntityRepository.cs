using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TrippleD.Core;
using TrippleD.Domain.Customers.Model;
using TrippleD.Domain.SharedKernel;
using TrippleD.Domain.SharedKernel.EventDispatcher;
using TrippleD.Domain.SharedKernel.Events;

namespace TrippleD.Persistence.Repository
{
    public abstract class EntityRepository<TEntity, TKey> : IEntityRepository<TEntity, TKey>
        where TEntity : AggregateRoot<TKey>
    {
        protected readonly IDomainEventDispatcher Dispatcher;
        private readonly InMemoryStore.InMemoryStore store;

        protected EntityRepository(InMemoryStore.InMemoryStore store, IDomainEventDispatcher dispatcher)
        {
            this.store = store;
            Dispatcher = dispatcher;
        }

        public void Add(TEntity entity)
        {
            store.Add(entity);
            DispatchEvents(entity);
        }

        public void Delete(TEntity entity)
        {
            store.Delete(entity);
            DispatchEvents(entity);
        }

        public IEnumerable<TEntity> GetEntities()
        {
            return store.GetEntities<TEntity>();
        }

        public IEnumerable<TEntity> GetEntities(ISpecification<TEntity> criteria)
        {
            return store.GetEntities<TEntity>().Where(criteria.IsSatisfiedBy);
        }

        public TEntity GetEntity(ISpecification<TEntity> specification)
        {
            return GetEntities(specification).FirstOrDefault();
        }

        public void Update(TEntity entity)
        {
            DispatchEvents(entity);
        }

        protected void DispatchEvents(TEntity entity)
        {
            Guard.ArgNotNull(entity, nameof(entity));

            foreach (IDomainEvent domainEvent in entity.DomainEvents)
            {
                Dispatcher.Dispatch(domainEvent);
            }
        }
    }

    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> SpecExpression { get; }

        bool IsSatisfiedBy(T entity);
    }

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

    public class CustomerIdSpecification : Specification<Customer>
    {
        private readonly int customerId;

        public CustomerIdSpecification(int customerId)
        {
            this.customerId = customerId;
        }

        public override Expression<Func<Customer, bool>> SpecExpression => c => c.Id == customerId;
    }

    public class CustomerEmailSpecification:Specification<Customer>
    {
        private readonly string email;

        public CustomerEmailSpecification(string email)
        {
            this.email = email;
        }

        public override Expression<Func<Customer, bool>> SpecExpression => c => c.Email == email;
    }

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
            Expression<Func<T, bool>> leftExpression = left.SpecExpression;
            Expression<Func<T, bool>> rightExpression = right.SpecExpression;

            BinaryExpression andExpression = Expression.AndAlso(leftExpression.Body, rightExpression.Body);

            return Expression.Lambda<Func<T, bool>>(andExpression, leftExpression.Parameters.Single());

            //var objParam = Expression.Parameter(typeof(T), "obj");

            //var newExpr = Expression.Lambda<Func<T, bool>>(
            //    Expression.AndAlso(
            //        Expression.Invoke(left.SpecExpression, objParam),
            //        Expression.Invoke(right.SpecExpression, objParam)
            //    ),
            //    objParam
            //);

            //return newExpr;
        }
    }

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
            Expression<Func<T, bool>> leftExpression = left.SpecExpression;
            Expression<Func<T, bool>> rightExpression = right.SpecExpression;

            BinaryExpression orExpression = Expression.OrElse(leftExpression.Body, rightExpression.Body);

            return Expression.Lambda<Func<T, bool>>(orExpression, leftExpression.Parameters.Single());
        }
    }

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

    public static class SpecificationExtensions
    {
        public static ISpecification<T> And<T>(
            this ISpecification<T> left,
            ISpecification<T> right)
        {
            return new AndSpecification<T>(left, right);
        }

        public static ISpecification<T> Or<T>(
            this ISpecification<T> left,
            ISpecification<T> right)
        {
            return new OrSpecification<T>(left, right);
        }

        public static ISpecification<T> Not<T>(this ISpecification<T> inner)
        {
            return new NotSpecification<T>(inner);
        }
    }
}
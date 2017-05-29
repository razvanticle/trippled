using System;
using System.Linq.Expressions;
using TrippleD.Domain.Customers.Model;
using TrippleD.Domain.SharedKernel.Specifications;

namespace TrippleD.Domain.Customers.Specifications
{
    public class CustomerIdSpecification : Specification<Customer>
    {
        private readonly int customerId;

        public CustomerIdSpecification(int customerId)
        {
            this.customerId = customerId;
        }

        public override Expression<Func<Customer, bool>> SpecExpression => c => c.Id == customerId;
    }
}
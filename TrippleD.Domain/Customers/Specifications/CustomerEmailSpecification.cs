using System;
using System.Linq.Expressions;
using TrippleD.Sales.Domain.Customers.Model;
using TrippleD.SharedKernel.Specifications;

namespace TrippleD.Sales.Domain.Customers.Specifications
{
    public class CustomerEmailSpecification : Specification<Customer>
    {
        private readonly string email;

        public CustomerEmailSpecification(string email)
        {
            this.email = email;
        }

        public override Expression<Func<Customer, bool>> SpecExpression => c => c.Email == email;
    }
}
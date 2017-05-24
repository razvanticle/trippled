using System;
using System.Collections.Generic;
using TrippleD.Domain.SharedKernel;

namespace TrippleD.Domain.Customers.Model
{
    public class Order : Entity<Guid>
    {
        public Order(Invoice invoice, IEnumerable<OrderItem> orderItems, PaymentMethod paymentMethod) : base(Guid.NewGuid())
        {
            Invoice = invoice;
            OrderItems = orderItems;
            PaymentMethod = paymentMethod;
        }

        public Invoice Invoice { get; }

        public IEnumerable<OrderItem> OrderItems { get; }

        public PaymentMethod PaymentMethod { get; }
    }
}
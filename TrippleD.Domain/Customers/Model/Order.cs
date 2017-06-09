using System;
using System.Collections.Generic;
using TrippleD.Domain.SharedKernel;
using TrippleD.Domain.SharedKernel.Identities;

namespace TrippleD.Domain.Customers.Model
{
    public class Order : Entity
    {
        public Order(Invoice invoice, IEnumerable<OrderItem> orderItems, PaymentMethod paymentMethod):base(Identity.Create())
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
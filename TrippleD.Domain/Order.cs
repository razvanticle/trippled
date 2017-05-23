using System.Collections.Generic;
using TrippleD.Domain.SharedKernel;

namespace TrippleD.Domain
{
    public class Order : ValueObjectBase<Order>
    {
        public Order(Invoice invoice, IEnumerable<OrderItem> orderItems, PaymentMethod paymentMethod)
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
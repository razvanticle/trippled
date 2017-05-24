using System;
using System.Collections.Generic;
using TrippleD.SharedKernel.Dtos;

namespace TrippleD.Customers.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }

        public AddressDto InvoiceAddress { get; set; }

        public IEnumerable<OrderItemDto> OrderItems { get; set; }

        public PaymentMethodDto PaymentMethod { get; set; }
    }
}
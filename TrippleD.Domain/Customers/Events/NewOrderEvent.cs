﻿using TrippleD.Domain.Customers.Model;
using TrippleD.Domain.SharedKernel.Events;

namespace TrippleD.Domain.Customers.Events
{
    public class NewOrderEvent : DomainEvent
    {
        public NewOrderEvent(Order order)
        {
            Order = order;
        }

        public Order Order { get; }
    }
}
﻿using TrippleD.SharedKernel;

namespace TrippleD.Sales.Domain.Customers.Model
{
    public class PaymentMethod : ValueObjectBase<PaymentMethod>
    {
        public PaymentMethod(PaymentMethodType type)
        {
            Type = type;
        }

        public PaymentMethodType Type { get; }
    }
}
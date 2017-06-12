﻿using TrippleD.SharedKernel;
using TrippleD.SharedKernel.Model;

namespace TrippleD.Sales.Domain.Customers.Model
{
    public class Invoice : ValueObjectBase<Invoice>
    {
        public Invoice(Address address)
        {
            Address = address;
        }

        public Address Address { get; }
    }
}
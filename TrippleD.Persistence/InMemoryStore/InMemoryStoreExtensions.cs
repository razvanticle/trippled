﻿using System;
using System.Collections.Generic;
using TrippleD.Domain.Companies.Model;
using TrippleD.Domain.Customers.Model;
using TrippleD.Domain.Products;
using TrippleD.Domain.SharedKernel;
using TrippleD.Domain.SharedKernel.Identities;
using TrippleD.Domain.SharedKernel.Model;

namespace TrippleD.Persistence.InMemoryStore
{
    public static class InMemoryStoreExtensions
    {
        public static void EnsureSeedData(this InMemoryStore store)
        {
            if (!store.Any<Company>())
            {
                store.AddRange(new Company(Identity.Create())
                    {
                        Name = "Company 1",
                        Address = new Address("Cluj", "5", "Garibaldi"),
                        Rating = 5,
                        BusinessHours = new TimeInterval(new Time(8, 0), new Time(17, 0)),
                        Email = "company@comany.com",
                        PhoneNumber = "123",
                        WebSite = "www.company",
                        Services = new List<Service>
                        {
                            new Service("service 1", 4),
                            new Service("service 2", 3),
                            new Service("service 3", 5)
                        }
                    },
                    new Company(Identity.Create()) {Name = "Company 2"},
                    new Company(Identity.Create()) {Name = "Company 3"});
            }

            if (!store.Any<Customer>())
            {
                store.AddRange(new Customer(Identity.Create(Constants.CustomerIds.Customer1))
                    {
                        Name = new Name("John", "Doe"),
                        Email = "jdoe@gmail.com",
                        MobilePhone = "123",
                        Orders = new List<Order>
                        {
                            new Order(new Invoice(new Address("cluj", "5", "garibaldi")), new List<OrderItem>
                                {
                                    new OrderItem(new Service("Service 1", 4), new Address("cluj", "5", "garibaldi"))
                                },
                                new PaymentMethod(PaymentMethodType.Cash))
                        }
                    },
                    new Customer(Identity.Create())
                    {
                        Name = new Name("Jane", "Doe"),
                        Email = "janedoe@gmail.com",
                        MobilePhone = "1232",
                        Orders = new List<Order>
                        {
                            new Order(new Invoice(new Address("cluj", "5", "garibaldi")), new List<OrderItem>
                                {
                                    new OrderItem(new Service("Service 2", 4), new Address("cluj", "5", "garibaldi"))
                                },
                                new PaymentMethod(PaymentMethodType.Cash))
                        }
                    });
            }

            if (!store.Any<Product>())
            {
                store.AddRange(new Product(Identity.Create(Constants.ProductIds.Product1), "prod 1", "prod 1"));
            }
        }
    }

    public static class Constants
    {
        public static class CustomerIds
        {
            public static Guid Customer1 = new Guid("76981478-a276-4bef-95e5-e56c3ccfadcb");
        }

        public static class ProductIds
        {
            public static Guid Product1 = new Guid("7ef0092f-9b17-465b-a5a9-4750933b6645");
        }

        public static class ProductRequestsIds
        {
            public static Guid Request1 = new Guid("caa375a0-5afb-4245-87fa-24890ade6be3");
        }
    }
}
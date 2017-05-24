using System.Collections.Generic;
using TrippleD.Domain.Company.Model;
using TrippleD.Domain.Customers.Model;
using TrippleD.Domain.SharedKernel;
using TrippleD.Domain.SharedKernel.Model;

namespace TrippleD.Persistence.InMemoryStore
{
    public static class InMemoryStoreExtensions
    {
        public static void EnsureSeedData(this InMemoryStore store)
        {
            if (!store.Any<Company>())
            {
                store.AddRange(new Company(1)
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
                    new Company(2) { Name = "Company 2" },
                    new Company(3) { Name = "Company 3" });
            }

            if (!store.Any<Customer>())
            {
                store.AddRange(new Customer(1)
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
                });
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using TrippleD.Persistence.Model;

namespace TrippleD.Persistence.Context
{
    public static class TrippleDContextExtensions
    {
        public static void EnsureSeedData(this TrippleDContext context)
        {
            if (!context.Companies.Any())
            {
                context.Companies.AddRange(new Company
                    {
                        Id = 1,
                        Name = "Company 1",
                        Address = new Address
                        {
                            Id = 1,
                            Street = "Garibaldi",
                            City = "Cluj-Napoca",
                            Number = "25"
                        },
                        Rating = 5,
                        OpenTime = new DateTime(1800, 01, 01, 8, 0, 0),
                        CloseTime = new DateTime(1800, 01, 01, 20, 0, 0),
                        Email = "company@comany.com",
                        PhoneNumber = "123",
                        WebSite = "www.company",
                        Services = new List<Service>
                        {
                            new Service
                            {
                                Id = 1,
                                Rating = 4,
                                Name = "service 1"
                            },
                            new Service
                            {
                                Id = 2,
                                Rating = 3,
                                Name = "service 2"
                            },
                            new Service
                            {
                                Id = 3,
                                Rating = 5,
                                Name = "service 3"
                            }
                        }
                    },
                    new Company
                    {
                        Id = 2,
                        Name = "Company 2"
                    },
                    new Company
                    {
                        Id = 3,
                        Name = "Company 3"
                    });

                context.SaveChanges();
            }
        }
    }
}
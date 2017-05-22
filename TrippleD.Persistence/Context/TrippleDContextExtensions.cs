using System.Collections.Generic;
using System.Linq;
using TrippleD.Domain.Company.Model;
using TrippleD.Domain.SharedKernel.Model;

namespace TrippleD.Persistence.Context
{
    public static class TrippleDContextExtensions
    {
        public static void EnsureSeedData(this TrippleDContext context)
        {
            if (!context.Companies.Any())
            {
                context.Companies.AddRange(new Company(1)
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
                    new Company(2) {Name = "Company 2"},
                    new Company(3) {Name = "Company 3"});

                context.SaveChanges();
            }
        }
    }
}
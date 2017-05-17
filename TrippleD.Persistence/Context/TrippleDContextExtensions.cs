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
                        Name = "Company 1"
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
            }
        }
    }
}
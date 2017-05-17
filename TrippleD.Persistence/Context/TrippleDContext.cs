using Microsoft.EntityFrameworkCore;
using TrippleD.Persistence.Model;

namespace TrippleD.Persistence.Context
{
    public class TrippleDContext : DbContext
    {
        public TrippleDContext(DbContextOptions<TrippleDContext> options)
            : base((DbContextOptions) options)
        {
        }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Service> Services { get; set; }
    }
}
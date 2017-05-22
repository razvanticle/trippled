using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TrippleD.Domain.Company.Model;
using TrippleD.Domain.SharedKernel.Model;
using TrippleD.Persistence.Repository;

namespace TrippleD.Persistence.Context
{
    public class TrippleDContext : DbContext
    {
        public TrippleDContext(DbContextOptions<TrippleDContext> options)
            : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Service> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().HasKey(x => x.Id);
            modelBuilder.Entity<Company>().Ignore(x => x.BusinessHours);

            modelBuilder.Entity<Service>().HasKey(x => new {x.Name, x.Rating});
            modelBuilder.Entity<Address>().HasKey(x => new {x.Street, x.City, x.Number});
        }
    }

    public class ContextProvider : IContextProvider
    {
        private readonly IServiceProvider serviceProvider;

        public ContextProvider(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public DbContext GetDbContext()
        {
            return serviceProvider.GetService<TrippleDContext>();
        }
    }
}
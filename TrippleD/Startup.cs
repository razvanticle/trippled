using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TrippleD.Domain.Company.Repositories;
using TrippleD.Domain.SharedKernel.Events;
using TrippleD.Persistence.Context;
using TrippleD.Persistence.Repository;

namespace TrippleD
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
            IApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                    .CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetService<TrippleDContext>();
                    context.EnsureSeedData();
                }
            }

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();

            applicationLifetime.ApplicationStopped.Register(() => this.ApplicationContainer.Dispose());
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TrippleDContext>(opt => opt.UseInMemoryDatabase());
            //services.AddScoped<IRepository, Repository>();

           // services.AddScoped<ICompanyRepository, CompanyRepository>();
            //services.AddSingleton<GenericDomainEventHandler>();

            // Add framework services.
            services.AddMvc();


            //var containerBuilder=new ContainerBuilder();

            //containerBuilder.RegisterType<Repository>().As<IRepository>();
            //containerBuilder.RegisterType<CompanyRepository>().As<ICompanyRepository>();
            //containerBuilder.Populate(services);
            //var container = containerBuilder.Build();

            //var autofacServiceProvider = new AutofacServiceProvider(container);
            //return autofacServiceProvider;

            IApplicationServicesBuilder applicationServicesBuilder = services.AddApplicationServices();
            ApplicationContainer = applicationServicesBuilder.Container;

            return applicationServicesBuilder.ServiceProvider;
        }
    }

    public static class ServiceCollectionExtensions
    {
        public static IApplicationServicesBuilder AddApplicationServices(this IServiceCollection services)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<Repository>().As<IRepository>();
            containerBuilder.RegisterType<CompanyRepository>().As<ICompanyRepository>();
            containerBuilder.Populate(services);
            var container = containerBuilder.Build();

            var serviceProvider = new AutofacServiceProvider(container);

            return new ApplicationServicesBuilder(serviceProvider, container);
        }
    }

    public interface IApplicationServicesBuilder
    {
        IServiceProvider ServiceProvider { get; }

        IContainer Container { get; }
    }

    public class ApplicationServicesBuilder : IApplicationServicesBuilder
    {
        public ApplicationServicesBuilder(IServiceProvider serviceProvider, IContainer container)
        {
            ServiceProvider = serviceProvider;
            Container = container;
        }

        public IServiceProvider ServiceProvider { get; }
        public IContainer Container { get; }
    }


}
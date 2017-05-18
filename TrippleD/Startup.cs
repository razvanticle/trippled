using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TrippleD.Domain.Company.Repositories;
using TrippleD.Persistence.Context;
using TrippleD.ServicesConfiguration.Extensions;
using TrippleD.ServicesConfiguration.RegistrationStrategies;

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

        public IContainer ApplicationContainer { get; private set; }

        public IConfigurationRoot Configuration { get; }

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

            applicationLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TrippleDContext>(opt => opt.UseInMemoryDatabase());
            services.AddMvc();

            var assemblies = new List<Assembly> {typeof(CompanyRepository).GetTypeInfo().Assembly};

            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);

            ApplicationContainer = containerBuilder
                .Execute<PersistenceRegistrationStrategy>()
                .Execute(() => ServicesRegistrationStrategy.Create(assemblies))
                .Build();

            return new AutofacServiceProvider(ApplicationContainer);
        }
    }
}
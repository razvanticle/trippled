using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using TrippleD.Core;
using TrippleD.Core.Extensions;

namespace TrippleD.ServicesConfiguration.RegistrationStrategies
{
    public sealed class ServicesRegistrationStrategy : IStrategy<ContainerBuilder>
    {
        private readonly IEnumerable<Assembly> assemblies;
        
        public ServicesRegistrationStrategy(
            IEnumerable<Assembly> assemblies)
        {
            this.assemblies = assemblies;
        }

        public void Execute(ContainerBuilder containerBuilder)
        {
            // todo refactor this
            var types = this.assemblies.SelectMany(a => a.GetTypes());

            foreach (var type in types)
            {
                var registrations = GetServicesFrom(type);
                foreach (var reg in registrations)
                {
                    RegisterService(reg, containerBuilder);
                }
            }
        }

        private void RegisterService(ServiceInfo registration, ContainerBuilder containerBuilder)
        {
            var registrationBuilder = containerBuilder.RegisterType(registration.To);

            if (string.IsNullOrEmpty(registration.ContractName))
            {
                registrationBuilder.As(registration.From);
            }
            else
            {
                registrationBuilder.As(registration.From).WithMetadata("Name", registration.ContractName);
            }

            var lifetimeBuilder = LifetimeFactory.CreateLifetimeRegistration(registration.InstanceLifetime);
            lifetimeBuilder(registrationBuilder);
        }

        public IEnumerable<ServiceInfo> GetServicesFrom(Type type)
        {
            var attributes = type.GetTypeInfo().GetAttributes<ServiceAttribute>(false);
            return attributes.Select(a => new ServiceInfo(a.ExportType ?? type, type, a.ContractName, a.Lifetime));
        }

        public static ServicesRegistrationStrategy Create(IEnumerable<Assembly> assemblies)
        {
            return new ServicesRegistrationStrategy(assemblies);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Builder;
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
            IEnumerable<Type> types = this.assemblies.SelectMany(a => a.GetTypes());

            foreach (Type type in types)
            {
                IEnumerable<ServiceInfo> registrations = GetServicesFrom(type);
                foreach (ServiceInfo reg in registrations)
                {
                    RegisterService(reg, containerBuilder);
                }
            }
        }

        private void RegisterService(ServiceInfo registration, ContainerBuilder containerBuilder)
        {
            IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> registrationBuilder = containerBuilder.RegisterType(registration.To);

            if (string.IsNullOrEmpty(registration.ContractName))
            {
                registrationBuilder.As(registration.From);
            }
            else
            {
                registrationBuilder.As(registration.From).WithMetadata("Name", registration.ContractName);
            }

            Action<IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle>> lifetimeBuilder = LifetimeFactory.CreateLifetimeRegistration(registration.InstanceLifetime);
            lifetimeBuilder(registrationBuilder);
        }

        public IEnumerable<ServiceInfo> GetServicesFrom(Type type)
        {
            IEnumerable<ServiceAttribute> attributes = type.GetTypeInfo().GetAttributes<ServiceAttribute>(false);
            return attributes.Select(a => new ServiceInfo(a.ExportType ?? type, type, a.ContractName, a.Lifetime));
        }

        public static ServicesRegistrationStrategy Create(IEnumerable<Assembly> assemblies)
        {
            return new ServicesRegistrationStrategy(assemblies);
        }
    }
}
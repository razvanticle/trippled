using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using TrippleD.Core;
using TrippleD.Core.Mappers;

namespace TrippleD.ServicesConfiguration.RegistrationStrategies
{
    public class MappersRegistrationStrategy : IStrategy<ContainerBuilder>
    {
        private readonly IEnumerable<Assembly> assemblies;

        public MappersRegistrationStrategy(IEnumerable<Assembly> assemblies)
        {
            this.assemblies = assemblies;
        }

        public static MappersRegistrationStrategy Create(IEnumerable<Assembly> assemblies)
        {
            return new MappersRegistrationStrategy(assemblies);
        }

        public void Execute(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterAssemblyTypes(assemblies.ToArray())
                .AsClosedTypesOf(typeof(IMapper<,>)).AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
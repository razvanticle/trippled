using Autofac;
using TrippleD.Core;
using TrippleD.Persistence.InMemoryStore;

namespace TrippleD.ServicesConfiguration.RegistrationStrategies
{
    public class PersistenceRegistrationStrategy : IStrategy<ContainerBuilder>
    {
        public void Execute(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<InMemoryStore>().SingleInstance();
        }
    }
}
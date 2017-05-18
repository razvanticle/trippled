using System;
using Autofac;
using TrippleD.Core;
using TrippleD.Core.Extensions;

namespace TrippleD.ServicesConfiguration.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder Execute<TStrategy>(this ContainerBuilder containerBuilder)
            where TStrategy : IStrategy<ContainerBuilder>, new()
        {
            return containerBuilder.Execute(() => new TStrategy());
        }

        public static ContainerBuilder Execute<TStrategy>(this ContainerBuilder containerBuilder,
            Func<TStrategy> strategyFactory) where TStrategy : IStrategy<ContainerBuilder>
        {
            var strategy = strategyFactory();
            return containerBuilder.Execute(strategy);
        }
    }
}
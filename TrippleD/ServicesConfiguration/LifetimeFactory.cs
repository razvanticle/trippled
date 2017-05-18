using System;
using System.Collections.Generic;
using Autofac.Builder;
using TrippleD.Core;

namespace TrippleD.ServicesConfiguration
{
    public static class LifetimeFactory
    {
        private static readonly
            Dictionary<Lifetime, Action<IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle>>> LifetimeBuilders =
                new Dictionary
                <Lifetime,
                    Action<IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle>>>
                {
                    { Lifetime.Application, registrationBuilder => registrationBuilder.SingleInstance() },
                    { Lifetime.AlwaysNew, registrationBuilder => registrationBuilder.InstancePerDependency() },
                    { Lifetime.Instance, registrationBuilder => registrationBuilder.InstancePerLifetimeScope() }
                };

        public static Action<IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle>> CreateLifetimeRegistration(Lifetime lifetime)
        {
            return LifetimeBuilders[lifetime];
        }
    }
}
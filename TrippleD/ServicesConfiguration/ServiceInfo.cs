using System;
using TrippleD.Core;

namespace TrippleD.ServicesConfiguration
{
    public class ServiceInfo
    {
        public ServiceInfo(Type from, Type to, Lifetime lifetime)
            : this(from, to, null, lifetime)
        {
        }

        public ServiceInfo(Type from, Type to, string contractName, Lifetime lifetime)
        {
            From = from;
            To = to;
            ContractName = contractName;
            InstanceLifetime = lifetime;
        }

        public string ContractName { get; }

        public Type From { get; }

        public Lifetime InstanceLifetime { get; }

        public Type To { get; }
    }
}
using System;

namespace TrippleD.SharedKernel.Identities
{
    public interface IIdentity
    {
        Guid Value { get; }
    }
}
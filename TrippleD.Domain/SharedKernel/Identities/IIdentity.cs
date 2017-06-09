using System;

namespace TrippleD.Domain.SharedKernel.Identities
{
    public interface IIdentity
    {
        Guid Value { get; }
    }
}
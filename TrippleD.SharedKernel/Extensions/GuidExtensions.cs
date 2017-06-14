using System;
using TrippleD.SharedKernel.Identities;

namespace TrippleD.SharedKernel.Extensions
{
    public static class GuidExtensions
    {
        public static IIdentity ToIdentity(this Guid guid)
        {
            return Identity.Create(guid);
        }
    }
}
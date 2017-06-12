using System;
using TrippleD.SharedKernel.Identities;

namespace TrippleD.Sales.Application.Extensions
{
    internal static class GuidExtensions
    {
        public static IIdentity ToIdentity(this Guid guid)
        {
            return Identity.Create(guid);
        }
    }
}
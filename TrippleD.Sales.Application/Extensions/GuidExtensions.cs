using System;
using TrippleD.Domain.SharedKernel.Identities;

namespace TrippleD.Application.Extensions
{
    internal static class GuidExtensions
    {
        public static IIdentity ToIdentity(this Guid guid)
        {
            return Identity.Create(guid);
        }
    }
}
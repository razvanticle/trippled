﻿
using TrippleD.SharedKernel.Events;
using TrippleD.SharedKernel.Identities;

namespace TrippleD.Sales.Domain.ProductRequests.Events
{
    public class RequestCanceledEvent : DomainEvent
    {
        public RequestCanceledEvent(IIdentity requestId)
        {
            RequestId = requestId;
        }

        public IIdentity RequestId { get; }
    }
}
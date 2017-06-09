using System;
using System.Diagnostics;

using TrippleD.Core;
using TrippleD.Domain.SharedKernel.Events;
using TrippleD.Domain.SharedKernel.Identities;

namespace TrippleD.Domain.Companies.Events
{
    public class ComanyRatingUpdatedEvent : DomainEvent
    {
        public ComanyRatingUpdatedEvent(decimal newRating, IIdentity companyId)
        {
            NewRating = newRating;
            CompanyId = companyId;
        }

        public decimal NewRating { get; }
        public IIdentity CompanyId { get; }
    }

    [Service(typeof(IDomainEventHandler<ComanyRatingUpdatedEvent>))]
    public class ComanyRatingUpdatedEventHandler : IDomainEventHandler<ComanyRatingUpdatedEvent>
    {
        public void Handle(ComanyRatingUpdatedEvent args)
        {
            Debug.Write($"Company {args.CompanyId} rating has been updated to {args.NewRating}");
        }
    }
}
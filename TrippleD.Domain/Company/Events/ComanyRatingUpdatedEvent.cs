using System.Diagnostics;
using TrippleD.Core;
using TrippleD.Domain.SharedKernel.Events;
using TrippleD.Persistence.Model;

namespace TrippleD.Domain.Company.Events
{
    public class ComanyRatingUpdatedEvent : DomainEvent
    {
        public ComanyRatingUpdatedEvent(decimal newRating, int companyId)
        {
            NewRating = newRating;
            CompanyId = companyId;
        }

        public decimal NewRating { get; }
        public int CompanyId { get; }
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
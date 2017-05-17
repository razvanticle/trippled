using System.Diagnostics;
using TrippleD.Domain.SharedKernel.Events;

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

    public class ComanyRatingUpdatedEventHandler : IDomainEventHandler<ComanyRatingUpdatedEvent>
    {
        public void Handle(ComanyRatingUpdatedEvent args)
        {
            Debug.Write($"Company {args.CompanyId} rating has been updated to {args.NewRating}");
        }
    }
}
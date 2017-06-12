using System;
using System.Collections.Generic;
using System.Linq;

using TrippleD.Domain.Companies.Events;
using TrippleD.Domain.SharedKernel;
using TrippleD.Domain.SharedKernel.Identities;
using TrippleD.Domain.SharedKernel.Model;

namespace TrippleD.Domain.Companies.Model
{
    public class Company : AggregateRoot
    {
        public Company(IIdentity id) : base(id)
        {
        }
        
        public Address Address { get; set; }

        public TimeInterval BusinessHours { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public decimal Rating { get; set; }

        public IList<Service> Services { get; set; }

        public string WebSite { get; set; }

        public void RateCompany(int value)
        {
            // todo
            Rating = (Rating + value) / 2;

            AddEvent(new ComanyRatingUpdatedEvent(Rating, Id));
        }

        public void RateService(Service service)
        {
            // todo
            Service serviceToRate = Services.FirstOrDefault(x => x.Equals(service));
            if (serviceToRate == null)
            {
                throw new Exception("Service not available for this company");
            }

            serviceToRate = service.Rate();
        }

        public void RemoveService(string serviceName)
        {
            Service service = Services.FirstOrDefault(x => x.Name == serviceName);

            Services.Remove(service);
        }

        public void RequestService()
        {
            // todo
        }
    }
}
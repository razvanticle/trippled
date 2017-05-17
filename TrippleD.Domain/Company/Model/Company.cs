﻿using System;
using System.Collections.Generic;
using System.Linq;
using TrippleD.Domain.Company.Events;
using TrippleD.Domain.SharedKernel;
using TrippleD.Domain.SharedKernel.Events;

namespace TrippleD.Domain.Company.Model
{
    public class Company : Entity<int>
    {
        public Company(int id) : base(id)
        {
            // todo add proper DI and remove this
            DomainEvents.Register<ComanyRatingUpdatedEvent>(new ComanyRatingUpdatedEventHandler().Handle);
        }

        public Address Address { get; set; }

        public TimeInterval BusinessHours { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public decimal Rating { get; set; }

        public IEnumerable<Service> Services { get; set; }

        public string WebSite { get; set; }

        public void RateCompany(int value)
        {
            // todo
            this.Rating = (Rating + value) / 2;

            DomainEvents.Raise(new ComanyRatingUpdatedEvent(Rating, Id));
        }

        public void RateService(Service service)
        {
            // todo
            var serviceToRate = Services.FirstOrDefault(x => x.Equals(service));
            if (serviceToRate == null)
            {
                throw new Exception("Service not available for this company");
            }

            serviceToRate = service.Rate();
        }

        public void RequestService()
        {
            // todo
        }
    }
}
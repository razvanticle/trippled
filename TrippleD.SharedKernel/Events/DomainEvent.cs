﻿using System;

namespace TrippleD.SharedKernel.Events
{
    public class DomainEvent : IDomainEvent
    {
        public DomainEvent()
        {
            DateTimeEventOccurred = DateTime.Now;
            Id = Guid.NewGuid();
        }

        public DateTime DateTimeEventOccurred { get; }
        public Guid Id { get; }
    }
}
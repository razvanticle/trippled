using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using TrippleD.Core;
using TrippleD.Domain.SharedKernel.Events;

namespace TrippleD.Domain.SharedKernel.EventDispatcher
{
    [Service(typeof(IDomainEventDispatcher), Lifetime.Application)]
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IServiceProvider serviceProvider;

        public DomainEventDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Dispatch(IDomainEvent domainEvent)
        {
            Guard.ArgNotNull(domainEvent, nameof(domainEvent));

            var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());
            var wrapperType = typeof(DomainEventHandler<>).MakeGenericType(domainEvent.GetType());
            var handlers = serviceProvider.GetServices(handlerType);

            var wrappedHandlers = handlers
                .Select(handler => (DomainEventHandler) Activator.CreateInstance(wrapperType, handler));

            foreach (var handler in wrappedHandlers)
            {
                handler.Handle(domainEvent);
            }
        }

        private abstract class DomainEventHandler
        {
            public abstract void Handle(IDomainEvent domainEvent);
        }

        private class DomainEventHandler<T> : DomainEventHandler
            where T : IDomainEvent
        {
            private readonly IDomainEventHandler<T> handler;

            public DomainEventHandler(IDomainEventHandler<T> handler)
            {
                this.handler = handler;
            }

            public override void Handle(IDomainEvent domainEvent)
            {
                handler.Handle((T) domainEvent);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Query.ResultOperators.Internal;
using Microsoft.Extensions.DependencyInjection;
using TrippleD.Domain.Company.Events;

namespace TrippleD.Domain.SharedKernel.Events
{
    public static class DomainEvents
    {
        [ThreadStatic]
        private static List<Delegate> actions;
        
        public static void Register<T>(Action<T> callback) where T : IDomainEvent
        {
            if (actions == null)
            {
                actions = new List<Delegate>();
            }
            actions.Add(callback);
        }

        public static void ClearCallbacks()
        {
            actions = null;
        }

        // todo add proer DI
        public static void Raise<T>(T args) where T : IDomainEvent
        {
            //foreach (var handler in Container.GetAllInstances<IDomainEventHandler<T>>())
            //{
            //    handler.Handle(args);
            //}

            

            if (actions != null)
            {
                foreach (var action in actions)
                {
                    if (action is Action<T>)
                    {
                        ((Action<T>)action)(args);
                    }
                }
            }
        }
    }

    public class GenericDomainEventHandler
    {
        private readonly IServiceProvider serviceProvider;
        private List<Delegate> actions;

        public GenericDomainEventHandler(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Register<T>(Action<T> callback) where T : IDomainEvent
        {
            if (actions == null)
            {
                actions = new List<Delegate>();
            }
            actions.Add(callback);
        }

        public void ClearCallbacks()
        {
            actions = null;
        }

        // todo add proer DI
        public void Raise<T>(T args) where T : IDomainEvent
        {
            var handlers = serviceProvider.GetServices<IDomainEventHandler<T>>();
            foreach (var handler in handlers)
            {
                handler.Handle(args);
            }

            if (actions != null)
            {
                foreach (var action in actions)
                {
                    if (action is Action<T>)
                    {
                        ((Action<T>)action)(args);
                    }
                }
            }
        }
    }
}
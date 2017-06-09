using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TrippleD.Persistence.InMemoryStore
{
    public class InMemoryStore : Dictionary<Type, IList>
    {
        public void Add<T>(T entity)
        {
            IList entities;
            if (!TryGetValue(typeof(T), out entities))
            {
                AddEntitiesCollection<T>(out entities);
            }

            entities.Add(entity);
        }

        public void AddRange<T>(params T[] newEntities)
        {
            IList entities;
            if (!TryGetValue(typeof(T), out entities))
            {
                AddEntitiesCollection<T>(out entities);
            }

            List<T> castedEntities = (List<T>) entities;
            castedEntities.AddRange(newEntities);
        }

        public bool Any<T>()
        {
            IList entities;
            if (TryGetValue(typeof(T), out entities))
            {
                return entities.Count > 0;
            }

            return false;
        }

        public void ClearEntities<T>()
        {
            IList entities;
            if (TryGetValue(typeof(T), out entities))
            {
                entities.Clear();
            }
        }

        public void Delete<T>(T entity)
        {
            Type entityType = typeof(T);

            IList entities;
            if (!TryGetValue(entityType, out entities))
            {
                throw new Exception($"The store does not contain a collection of type {entityType}");
            }

            entities.Remove(entity);
        }

        public IQueryable<T> GetEntities<T>()
        {
            IList entities;
            if (TryGetValue(typeof(T), out entities))
            {
                return (IQueryable<T>) entities.AsQueryable();
            }

            return Enumerable.Empty<T>().AsQueryable();
        }

        private void AddEntitiesCollection<T>(out IList entities)
        {
            entities = new List<T>();
            Add(typeof(T), entities);
        }
    }
}
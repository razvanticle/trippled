using System.Linq;
using Microsoft.EntityFrameworkCore;
using TrippleD.Persistence.Context;

namespace TrippleD.Persistence.Repository
{
    public class Repository : IRepository
    {
        private readonly TrippleDContext context;

        public Repository(TrippleDContext context)
        {
            this.context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            context.Entry(entity).State = EntityState.Added;
        }

        public void Delete<T>(T entity) where T : class
        {
            context.Entry(entity).State = EntityState.Deleted;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public T GetById<T>(params object[] keyValues) where T : class
        {
            return context.Set<T>().Find(keyValues);
        }

        public IQueryable<T> GetEntities<T>() where T : class
        {
            return context.Set<T>();
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void Update<T>(T entity) where T : class
        {
            context.Entry(entity).State = EntityState.Modified;
        }
    }
}
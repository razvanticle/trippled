using System;
using System.Linq;

namespace TripleD.Persistence.Repository
{
    public interface IRepository : IDisposable
    {
        void Add<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        T GetById<T>(params object[] keyValues) where T : class;

        IQueryable<T> GetEntities<T>() where T : class;

        void SaveChanges();

        void Update<T>(T entity) where T : class;
    }
}
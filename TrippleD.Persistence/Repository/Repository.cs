using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TrippleD.Persistence.Repository
{
    /// <summary>
    ///     Implements a repository for reading data with Entity Framework
    ///     The entities retrieved through this repository are not meant to be modified and persisted back.
    ///     This implementation is optimized for read-only operations. For reading data for edit, or delete create and use an
    ///     IUnitOfWork
    /// </summary>
    public class Repository : IRepository, IDisposable
    {
        private readonly IContextProvider contextProvider;


        public Repository(IContextProvider contextProvider)
        {
            this.contextProvider = contextProvider;
            Context = contextProvider.GetDbContext();
        }

        protected DbContext Context { get; }

        public IUnitOfWork CreateUnitOfWork()
        {
            return new UnitOfWork(contextProvider);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IQueryable<T> GetEntities<T>() where T : class
        {
            return Context.Set<T>().AsNoTracking();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Context?.Dispose();
            }
        }
    }

    public interface IContextProvider
    {
        DbContext GetDbContext();
    }
}
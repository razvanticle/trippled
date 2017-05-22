using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace TrippleD.Persistence.Repository
{
    /// <summary>
    ///     Implements an unit of work for modifying, deleting or adding new data with Entity Framework.
    ///     An instance of this class should have a well defined and short scope. It should be disposed once the changes were
    ///     saved into the database
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IContextProvider contextProvider;

        private readonly DbContext context;


        //private readonly IExceptionHandler exceptionHandler;

        private IDbContextTransaction transactionScope;

        internal UnitOfWork(
            IContextProvider contextProvider
            //IExceptionHandler exceptionHandler
        )
        {
            this.contextProvider = contextProvider;

            context = contextProvider.GetDbContext();
            //this.exceptionHandler = exceptionHandler;
        }

        public void Add<T>(T entity) where T : class
        {
            context.Set<T>().Add(entity);
        }

        public void BeginTransactionScope()
        {
            if (transactionScope != null)
            {
                throw new InvalidOperationException("Cannot begin another transaction scope");
            }

            transactionScope = context.Database.BeginTransaction();
        }

        public IUnitOfWork CreateUnitOfWork()
        {
            return this;
        }

        public void Delete<T>(T entity) where T : class
        {
            context.Set<T>().Remove(entity);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IQueryable<T> GetEntities<T>() where T : class
        {
            return context.Set<T>();
        }

        public void SaveChanges()
        {
            try
            {
                context.SaveChanges();
                transactionScope?.Commit();
            }
            catch (Exception e)
            {
                Handle(e);
            }
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await context.SaveChangesAsync();

                transactionScope?.Commit();
            }
            catch (Exception e)
            {
                Handle(e);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                transactionScope?.Dispose();
                context.Dispose();
            }
        }


        private void Handle(Exception exception)
        {
            //this.exceptionHandler.Handle(exception);
        }
    }
}
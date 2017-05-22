using System;
using System.Threading.Tasks;

namespace TrippleD.Persistence.Repository
{
    /// <summary>
    ///     A unit of work that allows to modify and save entities in the database
    /// </summary>
    public interface IUnitOfWork : IRepository, IDisposable
    {
        /// <summary>
        ///     Adds to the current unit of work a new entity of type T
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="entity">The entity to be added</param>
        void Add<T>(T entity) where T : class;

        /// <summary>
        ///     Begins a TransactionScope
        /// </summary>
        void BeginTransactionScope();

        /// <summary>
        ///     Deletes from the current unit of work an entity of type T
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="entity">The entity to be deleted</param>
        void Delete<T>(T entity) where T : class;

        

        /// <summary>
        ///     Saves the changes that were done on the entities on the current unit of work
        /// </summary>
        void SaveChanges();

        /// <summary>
        ///     Saves the changes that were done on the entities on the current unit of work
        /// </summary>
        Task SaveChangesAsync();
    }
}
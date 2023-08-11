using Bechelor.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bechelor.Infrastructure.Data
{
    public interface IEntityRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Get entity by id 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        /// <summary>
        /// Query interface
        /// </summary>
        /// <returns></returns>
        IQueryable<T> AllIQueryable();
        /// <summary>
        ///  Get Entity list
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);
        /// <summary>
        /// Get deleted entity list
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IReadOnlyList<T>> GetAllDeletedAsync(CancellationToken cancellationToken = default);
        /// <summary>
        /// paginated list
        /// </summary>
        /// <param name="recSkip"></param>
        /// <param name="recTake"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IReadOnlyList<T>> GetAllPagedAsync(int recSkip, int recTake, CancellationToken cancellationToken = default);
        /// <summary>
        /// Paginated list for deleted entity
        /// </summary>
        /// <param name="recSkip"></param>
        /// <param name="recTake"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IReadOnlyList<T>> GetDeletedPagedAsync(int recSkip, int recTake, CancellationToken cancellationToken = default);
        /// <summary>
        /// Add new entity to database
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
        /// <summary>
        /// Update existing entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default);
        /// <summary>
        /// Delete permanently... Can not be recovered again
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> PermanentDeleteAsync(T entity, CancellationToken cancellationToken = default);
        /// <summary>
        /// Delete entity by primary key..... can not be recovered
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> PermanentDeleteByIdAsync(int id, CancellationToken cancellationToken = default);
        /// <summary>
        /// Soft delete
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> SoftDeleteAsync(T entity, CancellationToken cancellationToken = default);
        /// <summary>
        /// Soft delete by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> SoftDeleteByIdAsync(int id, CancellationToken cancellationToken = default);
        /// <summary>
        /// Restore deleted entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> RestoreAsync(T entity, CancellationToken cancellationToken = default);
        /// <summary>
        /// Restore deleted by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> RestoreByIdAsync(int id, CancellationToken cancellationToken = default);
        /// <summary>
        /// Get first entity
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<T> FirstAsync(CancellationToken cancellationToken = default);
        /// <summary>
        /// Get first or default
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<T> FirstOrDefaultAsync(CancellationToken cancellationToken = default);
        /// <summary>
        /// COunt in table
        /// </summary>
        /// <returns></returns>
        int Count();
        /// <summary>
        /// Count in table async
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> CountAsync(CancellationToken cancellationToken = default);
    }
}

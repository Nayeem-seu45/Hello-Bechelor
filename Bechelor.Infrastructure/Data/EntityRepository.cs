using Bechelor.Core.Common;
using Bechelor.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bechelor.Infrastructure.Data
{
    public class EntityRepository<T> : IEntityRepository<T> where T : BaseEntity
    {
        #region ctor
        protected readonly BechelorContext context;
        protected readonly IWorkContext workContext;

        public EntityRepository(BechelorContext context, IWorkContext workContext)
        {
            this.context = context;
            this.workContext = workContext;
        }
        #endregion ctor
        #region methods
        public virtual async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await context.Set<T>().ToListAsync(cancellationToken);
        }

        public async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return await context.Set<T>().CountAsync(cancellationToken);
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            try
            {
                entity = await GetAddAsyncProperties(entity);
                await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync(cancellationToken);

                return entity;
            }
            catch (Exception ex)
            {
                entity = null;
                return entity;
            }

        }

        public async Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            entity = await GetUpdateAsyncProperties(entity);
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(true);
        }

        public async Task<bool> PermanentDeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(true);
        }

        public async Task<bool> PermanentDeleteByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            context.Set<T>().Remove(context.Set<T>().Find(id));
            await context.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(true);
        }

        public async Task<T> FirstAsync(CancellationToken cancellationToken = default)
        {
            return await context.Set<T>().FirstAsync(cancellationToken);
        }

        public async Task<T> FirstOrDefaultAsync(CancellationToken cancellationToken = default)
        {
            return await context.Set<T>().FirstOrDefaultAsync(cancellationToken);
        }

        public int Count()
        {
            return context.Set<T>().Count();
        }

        public async Task<IReadOnlyList<T>> GetAllPagedAsync(int recSkip, int recTake, CancellationToken cancellationToken = default)
        {
            return await context.Set<T>().Skip(recSkip).Take(recTake).ToListAsync(cancellationToken);
        }

        public IQueryable<T> AllIQueryable()
        {
            return context.Set<T>().AsQueryable();
        }
        public async Task<bool> SoftDeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            entity = await GetSoftDeleteAsyncProperties(entity);

            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(true);
        }

        public async Task<IReadOnlyList<T>> GetAllDeletedAsync(CancellationToken cancellationToken = default)
        {
            return await context.Set<T>().Where(s => s.IsSoftDeleted).ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<T>> GetDeletedPagedAsync(int recSkip, int recTake, CancellationToken cancellationToken = default)
        {
            return await context.Set<T>().Where(s => s.IsSoftDeleted).Skip(recSkip).Take(recTake).ToListAsync(cancellationToken);
        }

        public async Task<bool> SoftDeleteByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await context.Set<T>().FindAsync(id);
            entity = await GetSoftDeleteAsyncProperties(entity);
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(true);
        }

        public async Task<bool> RestoreAsync(T entity, CancellationToken cancellationToken = default)
        {
            entity = await GetRestoreAsyncProperties(entity);
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(true);
        }


        public async Task<bool> RestoreByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await context.Set<T>().FindAsync(id);
            entity = await GetRestoreAsyncProperties(entity);
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(true);
        }
        #endregion

        #region properties


        private async Task<T> GetAddAsyncProperties(T entity)
        {
            ApplicationUser user = await workContext.GetCurrentUserAsync();
            entity.CreatedOn = DateTime.Now;
            entity.CreatedBy = user.Id;
            entity.TanentId = user.MemberInformation.TanentId;
            return entity;
        }
        private async Task<T> GetUpdateAsyncProperties(T entity)
        {
            ApplicationUser user = await workContext.GetCurrentUserAsync();
            entity.UpdatedOn = DateTime.Now;
            entity.UpdatedBy = user.Id;
            entity.TanentId = user.MemberInformation.TanentId;

            return entity;
        }

        private async Task<T> GetSoftDeleteAsyncProperties(T entity)
        {
            ApplicationUser user = await workContext.GetCurrentUserAsync();
            entity.DeletedOn = DateTime.Now;
            entity.DeletedBy = user.Id;
            entity.IsSoftDeleted = true;
            return entity;
        }

        private async Task<T> GetRestoreAsyncProperties(T entity)
        {

            ApplicationUser user = await workContext.GetCurrentUserAsync();
            entity.IsSoftDeleted = false;
            entity.UpdatedBy = user.Id;
            entity.UpdatedOn = DateTime.Now;
            return entity;
        }
        #endregion
    }
}

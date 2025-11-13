using Microsoft.EntityFrameworkCore;
using Softcode.GTex.ExceptionHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Softcode.GTex.Data
{
    public abstract class BaseViewRepository<TDbContext, TEntity> : IViewRepository<TEntity>
        where TEntity : class
        where TDbContext : DbContext
    {
        protected readonly TDbContext Context;
        protected readonly DbQuery<TEntity> dbQuery;

        protected BaseViewRepository(TDbContext context)
        {
            Context = context ?? throw new SoftcodeArgumentMissingException(nameof(context));

            dbQuery = this.Context.Query<TEntity>();
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return dbQuery.AsQueryable();
        }

        public virtual async Task<ICollection<TEntity>> GetAllAsync()
        {

            return await Context.Set<TEntity>().ToListAsync();
        }

        public virtual IQueryable<TEntity> AsQueryable()
        {
            return dbQuery.AsQueryable();
        }

        public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return dbQuery.Where(predicate);
        }

        public virtual async Task<ICollection<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public virtual TEntity FindOne(Expression<Func<TEntity, bool>> predicate)
        {
            return dbQuery.FirstOrDefault(predicate);
        }
        public virtual async Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbQuery.FirstOrDefaultAsync(predicate);
        }


        public virtual bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return dbQuery.Any(predicate);
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbQuery.AnyAsync(predicate);
        }

        public virtual int Count()
        {
            return dbQuery.Count();
        }

        public virtual async Task<int> CountAsync()
        {
            return await dbQuery.CountAsync();
        }

        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return dbQuery.Count(predicate);
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbQuery.CountAsync(predicate);
        }
    }
}

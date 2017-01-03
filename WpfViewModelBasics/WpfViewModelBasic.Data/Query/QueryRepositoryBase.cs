using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WpfViewModelBasics.Core.Enums;
using WpfViewModelBasics.Core.Extensions;
using WpfViewModelBasics.Core.Initializer;
using WpfViewModelBasics.Core.Interfaces;
using WpfViewModelBasics.Core.Repository.Query;

namespace WpfViewModelBasic.Data.Query
{
	public class QueryRepositoryBase<T>: IQueryRepository<T>
		where T : class, IEntity
	{
		private readonly IDbSet<T> dbSet;

		public QueryRepositoryBase(IRepositoryInitializer<T> initializer)
		{
			if (initializer == null)
			{
				throw new ArgumentNullException(nameof(initializer));
			}
			this.dbSet = initializer.GetDbSet();
		}

        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await this.CreateQueryable().AnyAsync(predicate);
        }

        public virtual async Task<int> CountAsync()
        {
            return await this.CreateQueryable().CountAsync();
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await this.CreateQueryable().CountAsync(predicate);
        }

        public virtual async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await this.CreateQueryable()
                .Where(predicate)
                .ToListAsync();
        }

        public virtual async Task<List<T>> FindAsync<TI>(
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, TI>>[] includes)
        {
            return await this.CreateQueryable(includes).Where(predicate).ToListAsync();
        }

        public virtual async Task<T> FirstAsync(Expression<Func<T, bool>> predicate)
        {
            return await this.CreateQueryable().FirstAsync(predicate);
        }

        public virtual async Task<T> FirstAsync<TI>(
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, TI>>[] includes)
        {
            return await this.CreateQueryable(includes).FirstAsync(predicate);
        }

        public virtual async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await this.CreateQueryable().FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<T> FirstOrDefaultAsync<TI>(
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, TI>>[] includes)
        {
            return await this.CreateQueryable(includes).FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await this.CreateQueryable().ToListAsync();
        }

        public virtual async Task<List<T>> GetAllAsync<TI>(params Expression<Func<T, TI>>[] includes)
        {
            return await this.CreateQueryable(includes).ToListAsync();
        }

        public virtual async Task<List<T>> GetAllOrderedByAsync<TO>(
            Expression<Func<T, TO>> orderBy,
            Sort sort = Sort.Asc)
        {
            return await this.CreateQueryable()
                .OrderByWithDirection(orderBy, sort)
                .ToListAsync();
        }

        public virtual async Task<List<T>> GetAllOrderedByAsync<TO, TI>(
            Expression<Func<T, TO>> orderBy,
            Sort sort = Sort.Asc,
            params Expression<Func<T, TI>>[] includes
            )
        {
            return await this.CreateQueryable(includes)
                .OrderByWithDirection(orderBy, sort)
                .ToListAsync();
        }

        public virtual async Task<List<T>> GetAllWithPagingAsync<TO>(
            Expression<Func<T, TO>> orderBy,
            int pageIndex,
            int pageSize,
            Sort sort = Sort.Asc)
        {
            if (pageIndex < 0)
            {
                throw new ArgumentException(nameof(pageIndex));
            }
            if (pageSize < 0)
            {
                throw new ArgumentException(nameof(pageSize));
            }

            return await this.CreateQueryable()
                .OrderByWithDirection(orderBy, sort)
                .Skip(pageIndex * pageSize)
                .Take(pageSize).ToListAsync();
        }

        public virtual async Task<List<T>> GetAllWithPagingAsync<TO, TI>(
            Expression<Func<T, TO>> orderBy,
            int pageIndex,
            int pageSize,
            Sort sort = Sort.Asc,
            params Expression<Func<T, TI>>[] includes)
        {
            if (pageIndex < 0)
            {
                throw new ArgumentException(nameof(pageIndex));
            }
            if (pageSize < 0)
            {
                throw new ArgumentException(nameof(pageSize));
            }

            return await this.CreateQueryable(includes)
                .OrderByWithDirection(orderBy, sort)
                .Skip(pageIndex * pageSize)
                .Take(pageSize).ToListAsync();
        }

        public virtual async Task<List<T>> GetFilteredtWithPagingAndOrderAsync<TO>(
            Expression<Func<T, bool>> filter,
            Expression<Func<T, TO>> orderBy,
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            Sort sort = Sort.Asc)
        {
            if (pageIndex < 0)
            {
                throw new ArgumentException(nameof(pageIndex));
            }
            if (pageSize < 0)
            {
                throw new ArgumentException(nameof(pageSize));
            }

            return await this.CreateQueryable()
                .Where(filter)
                .OrderByWithDirection(orderBy, sort)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public virtual async Task<List<T>> GetFilteredtWithPagingAndOrderAsync<TO, TI>(
            Expression<Func<T, bool>> filter,
            Expression<Func<T, TO>> orderBy,
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            Sort sort = Sort.Asc,
            params Expression<Func<T, TI>>[] includes)
        {
            if (pageIndex < 0)
            {
                throw new ArgumentException(nameof(pageIndex));
            }
            if (pageSize < 0)
            {
                throw new ArgumentException(nameof(pageSize));
            }

            return await this.CreateQueryable(includes)
                .Where(filter)
                .OrderByWithDirection(orderBy, sort)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public virtual async Task<T> SingleAsync(Expression<Func<T, bool>> predicate)
        {
            return await this.CreateQueryable().SingleAsync(predicate);
        }

        public virtual async Task<T> SingleAsync<TI>(
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, TI>>[] includes)
        {
            return await this.CreateQueryable(includes).SingleAsync(predicate);
        }

        public virtual async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await this.CreateQueryable().SingleOrDefaultAsync(predicate);
        }

        public virtual async Task<T> SingleOrDefaultAsync<TI>(
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, TI>>[] includes)
        {
            return await this.CreateQueryable(includes).SingleOrDefaultAsync(predicate);
        }


        private IQueryable<T> CreateQueryable()
        {
            return this.dbSet.AsNoTracking();
        }

        private IQueryable<T> CreateQueryable<TI>(params Expression<Func<T, TI>>[] includes)
        {
            var queryable = this.dbSet.AsNoTracking();
            queryable = includes.Aggregate(queryable, (current, include) => current.Include(include));
            return queryable;
        }
    }
}

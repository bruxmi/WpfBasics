using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
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

		public virtual bool Any(Expression<Func<T, bool>> predicate)
		{
			return this.CreateQueryable().Any(predicate);
		}

		public virtual  int Count()
		{
			return this.CreateQueryable().Count();
		}

		public virtual int Count(Expression<Func<T, bool>> predicate)
		{
			return  this.CreateQueryable().Count(predicate);
		}

		public virtual  List<T> Find(Expression<Func<T, bool>> predicate)
		{
			return  this.CreateQueryable()
				.Where(predicate)
				.ToList();
		}

		public virtual  List<T> Find<TI>(
			Expression<Func<T, bool>> predicate,
			params Expression<Func<T, TI>>[] includes)
		{
			return  this.CreateQueryable(includes).Where(predicate).ToList();
		}

		public virtual T First(Expression<Func<T, bool>> predicate)
		{
			return  this.CreateQueryable().First(predicate);
		}

		public virtual  T First<TI>(
			Expression<Func<T, bool>> predicate,
			params Expression<Func<T, TI>>[] includes)
		{
			return  this.CreateQueryable(includes).First(predicate);
		}

		public virtual  T FirstOrDefault(Expression<Func<T, bool>> predicate)
		{
			return  this.CreateQueryable().FirstOrDefault(predicate);
		}

		public virtual  T FirstOrDefault<TI>(
			Expression<Func<T, bool>> predicate,
			params Expression<Func<T, TI>>[] includes)
		{
			return  this.CreateQueryable(includes).FirstOrDefault(predicate);
		}

		public virtual  List<T> GetAll()
		{
			return  this.CreateQueryable().ToList();
		}

		public virtual  List<T> GetAll<TI>(params Expression<Func<T, TI>>[] includes)
		{
			return  this.CreateQueryable(includes).ToList();
		}

		public virtual  List<T> GetAllOrderedBy<TO>(
			Expression<Func<T, TO>> orderBy,
			Sort sort = Sort.Asc)
		{
			return  this.CreateQueryable()
				.OrderByWithDirection(orderBy, sort)
				.ToList();
		}

		public virtual  List<T> GetAllOrderedBy<TO, TI>(
			Expression<Func<T, TO>> orderBy,
			Sort sort = Sort.Asc,
			params Expression<Func<T, TI>>[] includes
			)
		{
			return this.CreateQueryable(includes)
				.OrderByWithDirection(orderBy, sort)
				.ToList();
		}

		public virtual List<T> GetAllWithPaging<TO>(
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

			return  this.CreateQueryable()
				.OrderByWithDirection(orderBy, sort)
				.Skip(pageIndex * pageSize)
				.Take(pageSize).ToList();
		}

		public virtual List<T> GetAllWithPaging<TO, TI>(
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

			return  this.CreateQueryable(includes)
				.OrderByWithDirection(orderBy, sort)
				.Skip(pageIndex * pageSize)
				.Take(pageSize).ToList();
		}

		public virtual List<T> GetFilteredtWithPagingAndOrder<TO>(
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

			return this.CreateQueryable()
				.Where(filter)
				.OrderByWithDirection(orderBy, sort)
				.Skip(pageIndex * pageSize)
				.Take(pageSize)
				.ToList();
		}

		public virtual  List<T> GetFilteredtWithPagingAndOrder<TO, TI>(
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

			return this.CreateQueryable(includes)
				.Where(filter)
				.OrderByWithDirection(orderBy, sort)
				.Skip(pageIndex * pageSize)
				.Take(pageSize)
				.ToList();
		}

		public virtual  T Single(Expression<Func<T, bool>> predicate)
		{
			return  this.CreateQueryable().Single(predicate);
		}

		public virtual  T Single<TI>(
			Expression<Func<T, bool>> predicate,
			params Expression<Func<T, TI>>[] includes)
		{
			return  this.CreateQueryable(includes).Single(predicate);
		}

		public virtual T SingleOrDefault(Expression<Func<T, bool>> predicate)
		{
			return  this.CreateQueryable().SingleOrDefault(predicate);
		}

		public virtual T SingleOrDefault<TI>(
			Expression<Func<T, bool>> predicate,
			params Expression<Func<T, TI>>[] includes)
		{
			return  this.CreateQueryable(includes).SingleOrDefault(predicate);
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

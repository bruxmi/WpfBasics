using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WpfViewModelBasics.Core.Enums;
using WpfViewModelBasics.Core.Interfaces;

namespace WpfViewModelBasics.Core.Repository.Query
{
	/// <summary>
	///     This QueryRepository class shall be used to offer several query operations for single entities and lists.
	/// </summary>
	/// <typeparam name="T">The type of the entity class</typeparam>
	public interface IQueryRepository<T>
			where T : class, IEntity
	{
        /// <summary>
        /// Asynchronously determines whether any element of a sequence satisfies the given condition.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>Returns true whether there is one item that fulfills the given predicate, false if not.</returns>
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Asynchronously counts the items of a sequence.
        /// </summary>
        /// <returns>Returns the amount of items.</returns>
        Task<int> CountAsync();

        /// <summary>
        /// Asynchronously counts the items of a sequence that fulfills the given condition.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>Returns the amount of items.</returns>
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Asynchronously finds all items of a sequence that satisfy the given condition.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>Returns the list of items that fulfill the given predicate.</returns>
        Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Asynchronously finds all items of a sequence that satisfy the given condition and includes the given navigation property.
        /// </summary>
        /// <typeparam name="TI">The type of the navigation property.</typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <param name="includes">The navigation property to include.</param>
        /// <returns>Returns the list of items that fulfill the given predicate.</returns>
        Task<List<T>> FindAsync<TI>(
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, TI>>[] includes);

        /// <summary>
        /// Asynchronously returns the first element of a sequence that satisfies the specified condition.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>Returns the first Item that fulfills the given predicate, otherwise null</returns>
        Task<T> FirstAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Asynchronously returns the first element of a sequence that satisfies the specified condition and includes the specified navigation property.
        /// </summary>
        /// <typeparam name="TI">The type of the navigation property.</typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <param name="includes">The navigation property to include..</param>
        /// <returns>Returns the first elemment that satisfies the given condition.</returns>
        Task<T> FirstAsync<TI>(
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, TI>>[] includes);

        /// <summary>
        /// Asynchronously returns the first element of a sequence that satisfies the specified condition 
        /// or a default value if no such value is found.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Asynchronously returns the first element of a sequence that satisfies the specified condition 
        /// and includes the spicified navigation property or a default value if no such value is found.
        /// </summary>
        /// <typeparam name="TI">The type of the navigation property.</typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <param name="includes">The navigation property to include.</param>
        /// <returns>Returns the first element of a sequence that satisfies the specified condition 
        /// or a default value if no such value is found.</returns>
        Task<T> FirstOrDefaultAsync<TI>(
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, TI>>[] includes);

        /// <summary>
        /// Asynchronously returns a list of all items in the sequence.
        /// </summary>
        /// <returns>Returns the list of items.</returns>
        Task<List<T>> GetAllAsync();

        /// <summary>
        /// Asynchronously returns a list of all items in the sequence and includes the specified navigation property.
        /// </summary>
        /// <typeparam name="TI">The type of the navigation property.</typeparam>
        /// <param name="includes">The navigation property to inlcude.</param>
        /// <returns>Returns a list of all items in the sequence.</returns>
        Task<List<T>> GetAllAsync<TI>(params Expression<Func<T, TI>>[] includes);

        /// <summary>
        /// Asynchronously returns a sorted list of all items in the sequence per default in ascending order than can be overriden.
        /// </summary>
        /// <typeparam name="TO">The type of the navigation property to sort the list.</typeparam>
        /// <param name="orderBy">The navigation property to sort the list.</param>
        /// <param name="sort">The sort direction</param>
        /// <returns>Returns the sorted list.</returns>
        Task<List<T>> GetAllOrderedByAsync<TO>(Expression<Func<T, TO>> orderBy, Sort sort = Sort.Asc);

        /// <summary>
        /// Asynchronously returns a sorted list of all items in the sequence per default in ascending order than can be overriden
        /// and includes the specified navigation property.
        /// </summary>
        /// <typeparam name="TO">The type of the navigation property to sort the list.</typeparam>
        /// <typeparam name="TI">The type of the navigation property.</typeparam>
        /// <param name="orderBy">The navigation property to sort the list.</param>
        /// <param name="sort">The sort direction</param>
        /// <param name="includes">The navigation property to inlcude.</param>
        /// <returns>Returns the sorted list.</returns>
        Task<List<T>> GetAllOrderedByAsync<TO, TI>(
            Expression<Func<T, TO>> orderBy,
            Sort sort = Sort.Asc,
            params Expression<Func<T, TI>>[] includes);


        /// <summary>
        /// Asynchronously returns a paginated and sorted list per default in ascending order of items in the sequence.
        /// </summary>
        /// <param name="orderBy">The navigation property to sort the list.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sort">The sort direction</param>
        /// <returns>Returns the paginated and sorted list.</returns>
        Task<List<T>> GetAllWithPagingAsync<TO>(
            Expression<Func<T, TO>> orderBy,
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            Sort sort = Sort.Asc);

        /// <summary>
        /// Asynchronously returns a paginated and sorted list per default in ascending order of items in the sequence 
        /// and includes the specified navigation property.
        /// </summary>
        /// <typeparam name="TO">The type of the navigation property to sort the list.</typeparam>
        /// <typeparam name="TI">The type of the navigation property.</typeparam>
        /// <param name="orderBy">The navigation property to sort the list.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sort">The sort direction</param>
        /// <param name="includes">The navigation property to inlcude.</param>
        /// <returns>Returns the paginated and sorted list of items.</returns>
        Task<List<T>> GetAllWithPagingAsync<TO, TI>(
            Expression<Func<T, TO>> orderBy,
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            Sort sort = Sort.Asc,
            params Expression<Func<T, TI>>[] includes);

        /// <summary>
        /// Asynchronously returns a filtered, paginated and sorted list per default in ascending order of items in the sequence.
        /// </summary>
        /// <typeparam name="TO">The type of the navigation property to sort the list.</typeparam>
        /// <param name="filter">The condition to filter the list.</param>
        /// <param name="orderBy">The navigation property to sort the list.</param>
        /// <param name="sort">The sort direction</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>Returns the filtered, paginated and sorted list of items.</returns>
        Task<List<T>> GetFilteredtWithPagingAndOrderAsync<TO>(
            Expression<Func<T, bool>> filter,
            Expression<Func<T, TO>> orderBy,
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            Sort sort = Sort.Asc);

        /// <summary>
        /// Asynchronously returns a filtered, paginated and sorted list per default in ascending order of items in the sequence
        /// and includes the specified navigation property.
        /// </summary>
        /// <typeparam name="TO">The type of the navigation property to sort the list.</typeparam>
        /// <typeparam name="TI">The type of the navigation property.</typeparam>
        /// <param name="filter">The condition to filter the list.</param>
        /// <param name="orderBy">The navigation property to sort the list.</param>
        /// <param name="sort">The sort direction</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="includes">The navigation property to inlcude.</param>
        /// <returns>Returns the filtered, paginated and sorted list of items.</returns>
        Task<List<T>> GetFilteredtWithPagingAndOrderAsync<TO, TI>(
            Expression<Func<T, bool>> filter,
            Expression<Func<T, TO>> orderBy,
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            Sort sort = Sort.Asc,
            params Expression<Func<T, TI>>[] includes);

        /// <summary>
        /// Asynchronously returns the only element of the sequence that satisfies a given condition 
        /// and throws an exception if more than one such element exists.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>Returns the element that satisfies the given condition.</returns>
        Task<T> SingleAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Asynchronously returns the only element of the sequence that satisfies a given condition and includes the specified 
        /// navigation property and throws an exception if more than one such element exists.
        /// </summary>
        /// <typeparam name="TI">The type of the navigation property.</typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <param name="includes">The navigation property to inlcude.</param>
        /// <returns>Returns the element that satisfies the given condition.</returns>
        Task<T> SingleAsync<TI>(
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, TI>>[] includes);

        /// <summary>
        /// Asynchronously returns the only element of the sequence that satisfies a given condition 
        /// or a default value if no such value is found and throws an exception if more than one such element exists.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>Returns the element that satisfies the given condition.</returns>
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Asynchronously returns the only element of the sequence that satisfies a given condition
        /// and includes the specified navigation property
        /// or a default value if no such value is found 
        /// and throws an exception if more than one such element exists.
        /// </summary>
        /// <typeparam name="TI">The type of the navigation property.</typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <param name="includes">The navigation property to inlcude.</param>
        /// <returns>Returns the element that satisfies the given condition.</returns>
        Task<T> SingleOrDefaultAsync<TI>(
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, TI>>[] includes);
    }
}

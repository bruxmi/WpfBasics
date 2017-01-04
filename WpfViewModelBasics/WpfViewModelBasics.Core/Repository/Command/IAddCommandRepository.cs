using System.Collections.Generic;
using System.Threading.Tasks;

namespace WpfViewModelBasics.Core.Repository.Command
{
    /// <summary>
    ///     This IAddCommandRepository class shall be used to offer add operations for single entities and lists.
    /// </summary>
    /// <typeparam name="T">The type of the entity class</typeparam>
    public interface IAddCommandRepository<T>
        where T : class
    {
        Task<T> AddAsync(T entity);

        Task<ICollection<T>> AddListAsync(ICollection<T> entityList);
    }
}

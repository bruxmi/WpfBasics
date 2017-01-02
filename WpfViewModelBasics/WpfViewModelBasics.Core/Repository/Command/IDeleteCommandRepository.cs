using System.Collections.Generic;
using System.Threading.Tasks;

namespace WpfViewModelBasics.Core.Repository.Command
{
    /// <summary>
    ///     This IDeleteCommandRepository class shall be used to offer delete operations for single entities and lists.
    /// </summary>
    /// <typeparam name="T">The type of the entity class</typeparam>
    public interface IDeleteCommandRepository<T>
        where T : class
    {
        Task DeleteAsync(T entity);

        Task DeleteListAsync(ICollection<T> entityList);
    }
}
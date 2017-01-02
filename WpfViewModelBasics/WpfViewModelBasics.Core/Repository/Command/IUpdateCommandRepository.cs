using System.Collections.Generic;
using System.Threading.Tasks;

namespace WpfViewModelBasics.Core.Repository.Command
{
    /// <summary>
    ///     This IUpdateCommandRepository class shall be used to offer update operations for single entities and lists.
    /// </summary>
    /// <typeparam name="T">The type of the entity class</typeparam>
    public interface IUpdateCommandRepository<T>
        where T : class
    {
        Task UpdateAsync(T entity);

        Task UpdateListAsync(List<T> entities);
    }
}
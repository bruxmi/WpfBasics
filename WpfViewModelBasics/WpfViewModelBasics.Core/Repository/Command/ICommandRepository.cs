using WpfViewModelBasics.Core.Interfaces;

namespace WpfViewModelBasics.Core.Repository.Command
{
    /// <summary>
    ///     This ICommandRepository class shall be used to offer update, add and delete operations for single entities and lists.
    /// </summary>
    /// <typeparam name="T">The type of the entity class</typeparam>
    public interface ICommandRepository<T> : IUpdateCommandRepository<T>, IAddCommandRepository<T>, IDeleteCommandRepository<T>
		where T : class, IEntity
	{
	}
}

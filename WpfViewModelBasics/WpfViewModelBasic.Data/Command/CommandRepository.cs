using WpfViewModelBasics.Context;
using WpfViewModelBasics.Core.Interfaces;

namespace WpfViewModelBasic.Data.Command
{
    /// <summary>
    /// The Command repository that offers command operations on the DevContext. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CommandRepository<T> : CommandRepositoryBase<T>
        where T : class, IEntity
    {
        public CommandRepository(FriendStorageContext context)
            : base(context)
        {
        }
    }
}

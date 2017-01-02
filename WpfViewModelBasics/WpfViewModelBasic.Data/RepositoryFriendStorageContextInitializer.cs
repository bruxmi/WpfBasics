using WpfViewModelBasics.Context;
using WpfViewModelBasics.Core.Interfaces;

namespace WpfViewModelBasic.Data
{
    public class RepositoryFriendStorageContextInitializer<T> : RepositoryBaseInitializer<T>
        where T : class, IEntity
    {
        public RepositoryFriendStorageContextInitializer(FriendStorageContext context)
            : base(context)
        {

        }
    }
}

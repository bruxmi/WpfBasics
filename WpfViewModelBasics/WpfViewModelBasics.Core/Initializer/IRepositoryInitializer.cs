using System.Data.Entity;
using WpfViewModelBasics.Core.Interfaces;

namespace WpfViewModelBasics.Core.Initializer
{
    public interface IRepositoryInitializer<T>
            where T : class, IEntity
    {
        DbSet<T> GetDbSet();
    }
}

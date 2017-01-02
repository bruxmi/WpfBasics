using WpfViewModelBasics.Core.Initializer;
using WpfViewModelBasics.Core.Interfaces;

namespace WpfViewModelBasic.Data.Query
{
    public class QueryRepository<T> : QueryRepositoryBase<T>
        where T : class, IEntity
    {
        public QueryRepository(IRepositoryInitializer<T> initializer) :
            base(initializer)
        {
        }
    }
}

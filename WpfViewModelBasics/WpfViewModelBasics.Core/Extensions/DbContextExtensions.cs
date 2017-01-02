using System.Data.Entity;
using System.Linq;
using WpfViewModelBasics.Core.Interfaces;

namespace WpfViewModelBasics.Core.Extensions
{
	public static class DbContextExtensions
	{
		public static DbSet<T> GetDbSet<T>(this DbContext context)
			where T : class, IEntity
		{
			return (DbSet<T>)context
				.GetType()
				.GetProperties()
				.FirstOrDefault(p => p.PropertyType == typeof(DbSet<T>))
				?.GetValue(context, null);
		}
	}
}

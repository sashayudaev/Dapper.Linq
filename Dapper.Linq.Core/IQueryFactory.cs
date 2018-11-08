using System.Linq;
using System.Linq.Expressions;

namespace Dapper.Linq.Core
{
	public interface IQueryFactory
	{
		IQueryable<TEntity> CreateQuery<TEntity>(Expression expression)
			where TEntity : class;
		IQueryProvider CreateProvider<TEntity>()
			where TEntity : class;
	}
}

using System.Linq;
using System.Linq.Expressions;
using Dapper.Linq.Core;
using Dapper.Linq.Queries;

namespace Dapper.Linq.Factories
{
	public class QueryFactory : IQueryFactory
	{
		public IStorageContext Context { get; }

		public QueryFactory(IStorageContext context)
		{
			Context = context;
		}

		public IQueryable<TEntity> CreateQuery<TEntity>(Expression expression)
			where TEntity : class =>
			this.CreateProvider<TEntity>().CreateQuery<TEntity>(expression);

		public IQueryProvider CreateProvider<TEntity>()
			where TEntity : class =>
			new QueryProvider<TEntity>(Context);
	}
}

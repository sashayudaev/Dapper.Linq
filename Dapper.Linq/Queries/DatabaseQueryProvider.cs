using System.Linq.Expressions;
using Dapper.Linq.Configuration;
using Dapper.Linq.Core;

namespace Dapper.Linq.Queries
{
	public class DatabaseQueryProvider : QueryProvider
	{
		public DatabaseQueryProvider(IStorageContext context) 
			: base(context)
		{
		}

		public override TEntity Execute<TEntity>(Expression expression)
		{
			var mapper = DapperConfiguration.GetMapper<TEntity>();

			var queryBuilder = new QueryBuilder(mapper);

			var query = queryBuilder.Build(expression);
			var result = Connection.Query<TEntity>(query);

			return (TEntity) result;
		}
	}
}

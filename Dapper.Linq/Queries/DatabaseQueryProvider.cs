using System.Linq.Expressions;
using Dapper.Linq.Core;

namespace Dapper.Linq.Queries
{
	public class DatabaseQueryProvider : QueryProvider
	{
		public QueryBuilder QueryBuilder { get; }

		public DatabaseQueryProvider(IStorageContext context) 
			: base(context)
		{
			QueryBuilder = new QueryBuilder();
		}

		public override object Execute<TEntity>(Expression expression)
		{
			var query = QueryBuilder.Build(expression);
			var result = Connection.Query(typeof(TEntity), query);

			return result;
		}
	}
}

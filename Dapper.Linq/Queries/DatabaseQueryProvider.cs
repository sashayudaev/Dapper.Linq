using System.Data;
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

		public override TResult Execute<TResult>(Expression expression)
		{
			var query = QueryBuilder.Build(expression);
			var result = Connection.Query<TResult>(query);

			return default(TResult);
		}
	}
}

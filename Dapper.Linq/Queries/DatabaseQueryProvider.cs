using System.Linq.Expressions;
using Dapper.Linq.Core;
using Dapper.Linq.Tokens;

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
			var query = new QueryToken(typeof(TEntity));
			query.Build(expression);
			var result = Connection.Query<TEntity>(query.Value);

			return (TEntity) result;
		}
	}
}

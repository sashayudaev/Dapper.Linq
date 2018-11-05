using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Linq.Queries
{
	public class DatabaseQueryProvider : QueryProvider
	{
		public QueryBuilder QueryBuilder { get; }

		public DatabaseQueryProvider(IDbConnection connection) 
			: base(connection)
		{
			QueryBuilder = new QueryBuilder();
		}

		public override TResult Execute<TResult>(Expression expression)
		{
			var query = QueryBuilder.Build(expression);
			return Connection.Ex
		}
	}
}

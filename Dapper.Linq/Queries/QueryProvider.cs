using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace Dapper.Linq.Queries
{
	public abstract class QueryProvider : IQueryProvider
	{
		public IDbConnection Connection { get; }

		public QueryProvider(IDbConnection connection)
		{
			Connection = connection ?? 
				throw new ArgumentNullException(nameof(connection));
		}

		public IQueryable CreateQuery(Expression expression)
		{
			var query = (IQueryable) Activator
				.CreateInstance(typeof(Query<>)
				.MakeGenericType(expression.Type), new object[] { this, expression });

			return query;
		}

		public IQueryable<TEntity> CreateQuery<TEntity>(Expression expression) =>
			new Query<TEntity>(this, expression);

		public object Execute(Expression expression) =>
			this.Execute<object>(expression);

		public abstract TResult Execute<TResult>(Expression expression);
	}
}

using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using Dapper.Linq.Core;

namespace Dapper.Linq.Queries
{
	public abstract class QueryProvider : IQueryProvider
	{
		public IStorageContext Context { get; }
		public IDbConnection Connection { get; }

		public QueryProvider(IStorageContext context)
		{
			Context = context ?? 
				throw new ArgumentNullException(nameof(context));

			Connection = Context.ConfigureConnection();
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

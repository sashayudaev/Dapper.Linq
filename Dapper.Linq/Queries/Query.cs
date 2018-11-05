using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Dapper.Linq.Queries
{
	public class Query<TEntity> : 
		IQueryable<TEntity>, IQueryable,
		IEnumerable<TEntity>, IEnumerable,
		IOrderedQueryable<TEntity>, IOrderedQueryable
	{
		public Expression Expression { get; }
		public IQueryProvider Provider { get; }

		public Type ElementType =>
			typeof(TEntity);

		public Query(IQueryProvider provider, Expression expression)
		{
			Expression = expression ??
				Expression.Constant(this);
			Provider = provider ?? 
				throw new ArgumentNullException(nameof(provider));
		}

		IEnumerator<TEntity> GetEnumerator() =>
		 ((IEnumerable<TEntity>)Provider.Execute<TEntity>(Expression)).GetEnumerator();

		IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator() =>
			this.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() =>
			((IEnumerable)Provider.Execute(Expression)).GetEnumerator();

		public override string ToString() =>
			new QueryBuilder().Build(Expression);
	}
}

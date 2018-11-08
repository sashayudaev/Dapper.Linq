using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Dapper.Linq.Configuration;
using Dapper.Linq.Core;

namespace Dapper.Linq.Queries
{
	internal static class TypeHelper
	{
		internal static Type GetElementType(Type sequence)
		{
			Type enumerable = FindEnumerable(sequence);
			if (enumerable == null)
			{
				return sequence;
			}

			return enumerable.GetGenericArguments()[0];
		}
		private static Type FindEnumerable(Type sequence)
		{
			if (sequence == null || sequence == typeof(string))
			{
				return null;
			}

			if (sequence.IsArray)
			{
				var element = sequence.GetElementType();
				return typeof(IEnumerable<>).MakeGenericType(element);
			}

			if (sequence.IsGenericType)
			{
				foreach (Type arg in sequence.GetGenericArguments())
				{
					Type ienum = typeof(IEnumerable<>).MakeGenericType(arg);
					if (ienum.IsAssignableFrom(sequence))
					{
						return ienum;
					}
				}
			}
			var interfaces = sequence.GetInterfaces();
			if (interfaces != null && interfaces.Length > 0)
			{
				foreach (Type iface in interfaces)
				{
					Type ienum = FindEnumerable(iface);
					if (ienum != null)
					{

					}
				}
			}

			if (sequence.BaseType != null && 
				sequence.BaseType != typeof(object))
			{
				return FindEnumerable(sequence.BaseType);
			}

			return null;
		}
	}

	internal class QueryProvider<TEntity> : IQueryProvider
	{
		public IStorageContext Context { get; }

		public QueryProvider(IStorageContext context)
		{
			Context = context ?? 
				throw new ArgumentNullException(nameof(context));
		}

		public IQueryable CreateQuery(Expression expression)
		{
			var entity = TypeHelper.GetElementType(expression.Type);

			var query = (IQueryable) Activator
				.CreateInstance(typeof(Query<>)
				.MakeGenericType(expression.Type), new object[] { this, expression });

			return query;
		}

		public IQueryable<TElement> CreateQuery<TElement>(Expression expression) =>
			new Query<TElement>(this, expression);

		public TElement Execute<TElement>(Expression expression) =>
			(TElement)this.ExecuteInternal(expression);

		object IQueryProvider.Execute(Expression expression) =>
			this.ExecuteInternal(expression);

		private object ExecuteInternal(Expression expression)
		{
			var mapper = DapperConfiguration.GetMapper<TEntity>();
			var queryBuilder = new QueryBuilder(mapper);

			var sql = queryBuilder.Build(expression);

			return this.Query(sql);
		}

		private IEnumerable<TEntity> Query(string query)
		{
			using (var connection = Context.ConfigureConnection())
			{
				return connection.Query<TEntity>(query);
			}
		}
	}
}

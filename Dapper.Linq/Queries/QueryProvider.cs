using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using Dapper.Linq.Core;

namespace Dapper.Linq.Queries
{
	internal static class TypeSystem
	{
		internal static Type GetElementType(Type seqType)
		{
			Type ienum = FindIEnumerable(seqType);
			if (ienum == null) return seqType;
			return ienum.GetGenericArguments()[0];
		}
		private static Type FindIEnumerable(Type seqType)
		{
			if (seqType == null || seqType == typeof(string))
				return null;
			if (seqType.IsArray)
				return typeof(IEnumerable<>).MakeGenericType(seqType.GetElementType());
			if (seqType.IsGenericType)
			{
				foreach (Type arg in seqType.GetGenericArguments())
				{
					Type ienum = typeof(IEnumerable<>).MakeGenericType(arg);
					if (ienum.IsAssignableFrom(seqType))
					{
						return ienum;
					}
				}
			}
			Type[] ifaces = seqType.GetInterfaces();
			if (ifaces != null && ifaces.Length > 0)
			{
				foreach (Type iface in ifaces)
				{
					Type ienum = FindIEnumerable(iface);
					if (ienum != null) return ienum;
				}
			}
			if (seqType.BaseType != null && seqType.BaseType != typeof(object))
			{
				return FindIEnumerable(seqType.BaseType);
			}
			return null;
		}
	}

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
			var entity = TypeSystem.GetElementType(expression.Type);

			var query = (IQueryable) Activator
				.CreateInstance(typeof(Query<>)
				.MakeGenericType(expression.Type), new object[] { this, expression });

			return query;
		}

		public IQueryable<TEntity> CreateQuery<TEntity>(Expression expression) =>
			new Query<TEntity>(this, expression);

		TEntity IQueryProvider.Execute<TEntity>(Expression expression)
		{
			return (TEntity)this.Execute<TEntity>(expression);
		}

		object IQueryProvider.Execute(Expression expression)
		{
			return this.Execute<object>(expression);
		}

		public abstract object Execute<TEntity>(Expression expression);
	}
}

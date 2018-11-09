using System;
using System.Linq;
using System.Linq.Expressions;
using Dapper.Linq.Core;
using Dapper.Linq.Core.Mappers;
using Dapper.Linq.Helpers;
using Dapper.Linq.Tokens;
using Dapper.Linq.Tokens.Abstractions;

namespace Dapper.Linq.Queries
{
	public class QueryBuilder : ExpressionVisitor, IQueryBuilder
	{
		public IEntityMapper Mapper { get; }

		public PredicateCollection Predicates { get; } =
			new PredicateCollection();

		public QueryBuilder(IEntityMapper mapper)
		{
			Mapper = mapper ??
				throw new ArgumentNullException(nameof(mapper));
		}

		#region IQueryBuilder
		public string Build(Expression expression)
		{
			this.Visit(expression);
			return Predicates.Value;
		}
		#endregion

		protected override Expression VisitMethodCall(MethodCallExpression expression)
		{
			var method = expression.Method.Name;
			if (IsPredicate(method, out var predicateType))
			{
				var predicate = PredicateToken.Create(
					predicateType,
					Mapper,
					expression);

				Predicates.Add(predicate);
				this.VisitNext(expression);
			}

			return expression;
		}

		private static bool IsPredicate(string name, out PredicateType type) =>
			EnumHelper.TryGetFromDescription(name, out type);

		private void VisitNext(MethodCallExpression expression)
		{
			if (expression.Arguments.Count == 0)
			{
				return;
			}
			this.Visit(expression.Arguments[0]);
		}
	}
}

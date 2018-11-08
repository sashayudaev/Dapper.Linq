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

			this.AddSelectPredicate();
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
			if (!IsNotQueryable(expression))
			{
				throw new NotSupportedException(
					$"The method '{expression.Method.Name}' is not supported. " +
					$"Should be Queryable.");
			}

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

		private static bool IsNotQueryable(MethodCallExpression expression) =>
			typeof(Queryable).IsAssignableFrom(expression.Method.DeclaringType);

		private static bool IsPredicate(string name, out PredicateType type) =>
			EnumHelper.TryGetFromDescription(name, out type);

		private void AddSelectPredicate()
		{
			var select = new SelectToken(Mapper);
			Predicates.Add(select);
		}

		private void VisitNext(MethodCallExpression expression) =>
			this.Visit(expression.Arguments[0]);
	}
}

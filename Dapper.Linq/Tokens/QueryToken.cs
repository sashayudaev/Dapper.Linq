using System;
using System.Linq;
using System.Linq.Expressions;
using Dapper.Linq.Core;
using Dapper.Linq.Core.Tokens;
using Dapper.Linq.Helpers;
using Dapper.Linq.Tokens.Abstractions;

namespace Dapper.Linq.Tokens
{
	public class QueryToken : ExpressionVisitor, IQueryBuilder, IToken
	{
		public bool IsValid => true;

		public string Value =>
			Predicates.Value;

		public PredicateCollection Predicates { get; } =
			new PredicateCollection();

		public QueryToken(Type entity)
		{
			this.AddSelectPredicate(entity);
		}

		#region IQueryBuilder
		public void Build(Expression expression)
		{
			this.Visit(expression);
		}
		#endregion

		protected override Expression VisitMethodCall(MethodCallExpression expression)
		{
			if (!IsQueryable(expression))
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
					expression);

				Predicates.Add(predicate);
				this.VisitNext(expression);
			}

			return expression;
		}

		private static bool IsQueryable(MethodCallExpression expression) =>
			typeof(Queryable).IsAssignableFrom(expression.Method.DeclaringType);

		private static bool IsPredicate(string name, out PredicateType type) =>
			EnumHelper.TryGetFromDescription(name, out type);

		private void AddSelectPredicate(Type entity)
		{
			var select = new SelectToken(entity);
			Predicates.Add(select);
		}

		private void VisitNext(MethodCallExpression expression) =>
			this.Visit(expression.Arguments[0]);
	}
}

using System;
using System.Linq;
using System.Linq.Expressions;
using Dapper.Linq.Core;
using Dapper.Linq.Helpers;
using Dapper.Linq.Predicates;

namespace Dapper.Linq.Queries
{
	public class QueryBuilder : ExpressionVisitor
	{
		public PredicateCollection Predicates { get; } =
			new PredicateCollection();

		public string Build(Expression expression)
		{
			this.Visit(expression);
			return Predicates.BuildQuery();
		}

		protected override Expression VisitMethodCall(MethodCallExpression expression)
		{
			if(!IsQueryable(expression))
			{
				throw new NotSupportedException(
					$"The method '{expression.Method.Name}' is not supported. " +
					$"Should be Queryable.");
			}

			var method = expression.Method.Name;
			if(IsPredicate(method, out var predicateType))
			{
				var predicate = PredicateBase.Create(
					predicateType, 
					expression);

				Predicates.Add(predicate);
				this.VisitNext(expression);
			}

			return expression;
		}

		protected override Expression VisitConstant(ConstantExpression constant)
		{
			if (constant.Value is IQueryable queryable)
			{
				var predicate = PredicateBase.Create(
					PredicateType.Select,
					constant);

				Predicates.Add(predicate);
			}

			return constant;
		}

		private static bool IsQueryable(MethodCallExpression expression) =>
			typeof(Queryable).IsAssignableFrom(expression.Method.DeclaringType);

		private static bool IsPredicate(string name, out PredicateType type) =>
			EnumHelper.TryGetFromDescription(name, out type);

		private void VisitNext(MethodCallExpression expression) =>
			this.Visit(expression.Arguments[0]);
	}
}

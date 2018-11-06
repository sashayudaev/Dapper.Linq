using System;
using System.Linq.Expressions;
using Dapper.Linq.Core;
using Dapper.Linq.Core.Tokens;
using Dapper.Linq.Helpers;

namespace Dapper.Linq.Tokens.Abstractions
{
	public abstract class PredicateToken : ExpressionToken<MethodCallExpression>, IPredicateToken
	{
		public override bool IsValid =>
			base.IsValid &&
			PredicateType.OrdinalEquals(PredicateName);

		public string PredicateName =>
			Expression.Method.Name;

		public abstract PredicateType PredicateType { get; }

		public PredicateToken(MethodCallExpression expression)
			:base(expression)
		{
		}

		public static IPredicateToken Create(PredicateType type, MethodCallExpression expression)
		{
			switch (type)
			{
				case PredicateType.Where:
					return new WhereToken(expression);
				case PredicateType.OrderBy:
					return new OrderByToken(expression);
				case PredicateType.OrderByDescending:
					return new OrderByToken(expression, descending: true);
				case PredicateType.Take:
					return new TakeToken(expression);
				case PredicateType.Select:
				default:
					throw new InvalidOperationException(
						$"Predicate {type} does not exists");
			}
		}

		protected static TExpression Convert<TExpression>(Expression expression)
			where TExpression : Expression
		{
			while (expression.NodeType == ExpressionType.Quote)
			{
				expression = ((UnaryExpression)expression).Operand;
			}
			return (TExpression)expression;
		}
	}
}

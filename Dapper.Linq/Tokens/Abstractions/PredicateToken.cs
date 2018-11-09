using System;
using System.Linq.Expressions;
using Dapper.Linq.Core;
using Dapper.Linq.Core.Mappers;
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

		public PredicateToken(MethodCallExpression expression, IEntityMapper mapper)
			:base(expression, mapper)
		{
		}

		public static IPredicateToken Create(
			PredicateType type,
			IEntityMapper mapper,
			MethodCallExpression expression)
		{
			switch (type)
			{
				case PredicateType.Select:
					return new SelectToken(expression, mapper);
				case PredicateType.Insert:
					return new InsertToken(expression, mapper);
				case PredicateType.Where:
					return new WhereToken(expression, mapper);
				case PredicateType.GroupBy:
					return new GroupByToken(expression, mapper);
				case PredicateType.OrderBy:
					return new OrderByToken(expression, mapper);
				case PredicateType.OrderByDescending:
					return new OrderByToken(expression, mapper, descending: true);
				case PredicateType.Take:
					return new TakeToken(expression, mapper);
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

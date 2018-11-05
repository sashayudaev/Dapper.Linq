using System.Linq.Expressions;
using Dapper.Linq.Core;

namespace Dapper.Linq.Predicates
{
	public class OrderByPredicate : PredicateBase
	{
		public override PredicateType PredicateType =>
			PredicateType.OrderBy;

		public bool Descending { get; }

		public new MethodCallExpression Expression =>
			(MethodCallExpression)base.Expression;

		public OrderByPredicate(Expression expression)
			: this(expression, false)
		{

		}
		public OrderByPredicate(Expression expression, bool descending)
			: base(expression)
		{
			Descending = descending;
		}

		protected override Expression VisitMethodCall(MethodCallExpression expression)
		{
			Query.Append(" ORDER BY ");
			var argument = expression.Arguments[1];

			var lambda = Convert<LambdaExpression>(argument);
			this.Visit(lambda.Body);

			if(Descending)
			{
				Query.Append("DESC ");
			}

			return expression;
		}
	}
}

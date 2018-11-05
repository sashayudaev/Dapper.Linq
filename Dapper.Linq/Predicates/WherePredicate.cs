using System.Linq.Expressions;
using Dapper.Linq.Core;

namespace Dapper.Linq.Predicates
{
	public class WherePredicate : PredicateBase
	{
		public override PredicateType PredicateType =>
			PredicateType.Where;

		public new MethodCallExpression Expression =>
			(MethodCallExpression)base.Expression;

		public WherePredicate(Expression expression)
			:base(expression)
		{

		}

		protected override Expression VisitMethodCall(MethodCallExpression expression)
		{
			Query.Append(" WHERE ");

			var argument = expression.Arguments[1];
			var lambda = Convert<LambdaExpression>(argument);
			this.Visit(lambda.Body);

			return expression;
		}
	}
}

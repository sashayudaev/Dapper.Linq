using System.Linq.Expressions;
using Dapper.Linq.Core;

namespace Dapper.Linq.Predicates
{
	public class WherePredicate : PredicateBase
	{
		public override PredicateType PredicateType =>
			PredicateType.Where;

		public WherePredicate(MethodCallExpression expression)
			:base(expression)
		{

		}

		protected override Expression VisitMethodCall(MethodCallExpression expression)
		{
			Query.Append(" WHERE ");
			var argument = expression.Arguments[1];

			var lambda = RemoveQuote<LambdaExpression>(argument);
			this.Visit(lambda.Body);
			return expression;
		}
	}
}

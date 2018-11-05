using System.Linq.Expressions;
using Dapper.Linq.Core;

namespace Dapper.Linq.Predicates
{
	public class OrderByPredicate : PredicateBase
	{
		public override PredicateType PredicateType =>
			PredicateType.OrderBy;

		public OrderByPredicate(MethodCallExpression expression)
			: base(expression)
		{

		}

		protected override Expression VisitMethodCall(MethodCallExpression expression)
		{
			Query.Append(" ORDER BY ");
			LambdaExpression lambda = (LambdaExpression)StripQuotes(expression.Arguments[1]);
			this.Visit(lambda.Body);
			return expression;
		}
	}
}

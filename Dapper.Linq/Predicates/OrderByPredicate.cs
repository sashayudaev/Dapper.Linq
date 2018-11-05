using System.Linq.Expressions;
using Dapper.Linq.Core;

namespace Dapper.Linq.Predicates
{
	public class OrderByPredicate : PredicateBase
	{
		public override PredicateType PredicateType =>
			PredicateType.OrderBy;

		public bool Descending { get; }

		public OrderByPredicate(MethodCallExpression expression)
			: this(expression, false)
		{

		}
		public OrderByPredicate(MethodCallExpression expression, bool descending)
			: base(expression)
		{
			Descending = descending;
		}

		protected override Expression VisitMethodCall(MethodCallExpression expression)
		{
			Query.Append(" ORDER BY ");
			LambdaExpression lambda = (LambdaExpression)RemoveQuote(expression.Arguments[1]);
			this.Visit(lambda.Body);

			if(Descending)
			{
				Query.Append("DESC ");
			}

			return expression;
		}
	}
}

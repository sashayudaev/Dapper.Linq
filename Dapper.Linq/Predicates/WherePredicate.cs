using System;
using System.Linq.Expressions;
using System.Text;
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
			LambdaExpression lambda = (LambdaExpression)StripQuotes(expression.Arguments[1]);
			this.Visit(lambda.Body);
			return expression;
		}
	}
}

using System.Linq.Expressions;
using Dapper.Linq.Core;

namespace Dapper.Linq.Predicates
{
	public class TakePredicate : PredicateBase
	{
		public override PredicateType PredicateType { get; } =
			PredicateType.Take;

		public new MethodCallExpression Expression =>
			(MethodCallExpression)base.Expression;

		public TakePredicate(Expression expression)
			: base(expression)
		{
		}

		protected override Expression VisitMethodCall(MethodCallExpression expression)
		{
			var argument = expression.Arguments[1];
			var constant = Convert<ConstantExpression>(argument);

			Query.Append($" LIMIT {constant.Value}");
			return expression;
		}
	}
}

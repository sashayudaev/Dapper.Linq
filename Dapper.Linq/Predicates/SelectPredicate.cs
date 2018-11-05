using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dapper.Linq.Core;

namespace Dapper.Linq.Predicates
{
	public class SelectPredicate : PredicateBase
	{
		public override PredicateType PredicateType =>
			PredicateType.Select;

		public new ConstantExpression Expression =>
			(ConstantExpression)base.Expression;

		public SelectPredicate(Expression expression)
			: base(expression)
		{

		}

		protected override Expression VisitConstant(ConstantExpression constant)
		{
			if(constant.Value is IQueryable queryable)
			{
				var table = queryable.ElementType.Name;
				Query.Append($"SELECT * FROM {table}");
			}
			return constant;
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

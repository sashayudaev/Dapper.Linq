using System.Linq.Expressions;
using Dapper.Linq.Core;
using Dapper.Linq.Tokens.Abstractions;

namespace Dapper.Linq.Tokens
{
	public class WhereToken : PredicateToken
	{
		public override PredicateType PredicateType =>
			PredicateType.Where;

		public override string Value
		{
			get
			{
				Query.Append(" WHERE ");

				var argument = Expression.Arguments[1];
				var lambda = Convert<LambdaExpression>(argument);
				this.Visit(lambda.Body);

				return Query.ToString();
			}
		}

		public WhereToken(MethodCallExpression expression) 
			: base(expression)
		{
		}
	}
}

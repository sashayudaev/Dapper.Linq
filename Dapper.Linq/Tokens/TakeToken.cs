using System;
using System.Linq.Expressions;
using Dapper.Linq.Core;
using Dapper.Linq.Tokens.Abstractions;

namespace Dapper.Linq.Tokens
{
	public class TakeToken : PredicateToken
	{
		public override PredicateType PredicateType =>
			PredicateType.Take;

		public object Count
		{
			get
			{
				var argument = Expression.Arguments[1];
				var constant = Convert<ConstantExpression>(argument);

				return constant.Value;
			}
		}

		public override string Value =>
			$" LIMIT {Count}";

		public TakeToken(MethodCallExpression expression) 
			: base(expression)
		{
		}
	}
}

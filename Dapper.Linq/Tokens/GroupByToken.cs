using System.Linq.Expressions;
using Dapper.Linq.Core;
using Dapper.Linq.Core.Mappers;
using Dapper.Linq.Tokens.Abstractions;

namespace Dapper.Linq.Tokens
{
	public class GroupByToken : PredicateToken
	{
		public override PredicateType PredicateType =>
			PredicateType.GropuBy;

		public override string Value
		{
			get
			{
				Query.Append(" GROUP BY ");
				var argument = Expression.Arguments[1];

				var lambda = Convert<LambdaExpression>(argument);
				this.Visit(lambda.Body);

				return Query.ToString();
			}
		}

		public GroupByToken(MethodCallExpression expression, IEntityMapper mapper)
			: base(expression, mapper)
		{
		}
	}
}

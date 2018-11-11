using System;
using System.Linq.Expressions;
using Dapper.Linq.Core;
using Dapper.Linq.Core.Mappers;
using Dapper.Linq.Tokens.Abstractions;

namespace Dapper.Linq.Tokens
{
	public class OrderByToken : PredicateToken
	{
		public bool Descending { get; }

		public override PredicateType PredicateType =>
			PredicateType.OrderBy;

		public override string Value
		{
			get
			{
				Query.Append(" ORDER BY ");
				var argument = Expression.Arguments[1];

				var lambda = Convert<LambdaExpression>(argument);
				this.Visit(lambda.Body);

				if (Descending)
				{
					Query.Append(" DESC");
				}

				return Query.ToString();
			}
		}

		public OrderByToken(MethodCallExpression expression, IEntityMapper mapper)
			: this(expression, mapper, descending: false)
		{

		}

		public OrderByToken(MethodCallExpression expression, 
			IEntityMapper mapper, bool descending) : base(expression, mapper)
		{
			Descending = descending;
		}
	}
}

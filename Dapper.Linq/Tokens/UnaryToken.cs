using System;
using System.Linq.Expressions;
using Dapper.Linq.Core.Mappers;
using Dapper.Linq.Tokens.Abstractions;

namespace Dapper.Linq.Tokens
{
	public class UnaryToken : ExpressionToken<UnaryExpression>
	{
		public Expression Operand =>
			Expression.Operand;
		public ExpressionType Operation =>
			Expression.NodeType;

		public UnaryToken(UnaryExpression expression, IEntityMapper mapper) 
			: base(expression, mapper)
		{
		}

		public override string Value
		{
			get
			{
				switch (Operation)
				{
					case ExpressionType.Not:
						Query.Append(" NOT ");
						this.Visit(Operand);
						break;
					case ExpressionType.Quote:
						this.Visit(Operand);
						break;
					default:
						throw new NotSupportedException(
							$"The unary operator '{Operation}' is not supported");
				}

				return Query.ToString();
			}
		}
	}
}

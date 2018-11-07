using System.Linq.Expressions;
using Dapper.Linq.Core.Mappers;
using Dapper.Linq.Tokens.Abstractions;

namespace Dapper.Linq.Tokens
{
	public class BinaryToken : ExpressionToken<BinaryExpression>
	{
		public Expression Left =>
			Expression.Left;
		public Expression Right=>
			Expression.Right;

		public BinaryToken(BinaryExpression expression, IEntityMapper mapper) 
			: base(expression, mapper)
		{
		}

		public override string Value
		{
			get
			{
				Query.Append("(");
				this.Visit(Left);
				this.VisitOperation(Expression);
				this.Visit(Right);
				Query.Append(")");

				return Query.ToString();
			}
		}
	}
}

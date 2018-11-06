using System.Linq.Expressions;
using Dapper.Linq.Tokens.Abstractions;

namespace Dapper.Linq.Tokens
{
	public class BinaryToken : ExpressionToken<BinaryExpression>
	{
		public Expression Left =>
			Expression.Left;
		public Expression Right=>
			Expression.Right;

		public BinaryToken(BinaryExpression expression) 
			: base(expression)
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

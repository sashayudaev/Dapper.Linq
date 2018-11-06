using System.Linq.Expressions;
using System.Text;
using Dapper.Linq.Core.Tokens;

namespace Dapper.Linq.Tokens.Abstractions
{
	public abstract class ExpressionToken<TExpression> : 
		ExpressionVisitor, 
		IExpressionToken<TExpression>, IToken
		where TExpression : Expression
	{
		public virtual bool IsValid =>
			Expression != null;

		public StringBuilder Query { get; }

		public abstract string Value { get; }
		public TExpression Expression { get; }

		public ExpressionToken(TExpression expression)
		{
			Expression = expression;
			Query = new StringBuilder();
		}

		protected override Expression VisitUnary(UnaryExpression expression) =>
			this.VisitExpression(
				new UnaryToken(expression), 
				expression);

		protected override Expression VisitBinary(BinaryExpression expression) =>
			this.VisitExpression(
				new BinaryToken(expression), 
				expression);

		protected override Expression VisitConstant(ConstantExpression expression) =>
			this.VisitExpression(
				new ConstantToken(expression), 
				expression);

		protected override Expression VisitMember(MemberExpression expression) =>
			this.VisitExpression(
				new PropertyToken(expression), 
				expression);

		protected void VisitOperation(BinaryExpression expression)
		{
			var operation = new OperationToken(expression);
			Query.Append(operation.Value);
		}

		private Expression VisitExpression(
			IToken token,
			Expression expression)
		{
			Query.Append(token.Value);
			return expression;
		}
	}
}

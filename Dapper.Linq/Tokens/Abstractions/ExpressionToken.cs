using System.Linq.Expressions;
using System.Text;
using Dapper.Linq.Core.Mappers;
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
		public IEntityMapper Mapper { get; }
		public TExpression Expression { get; }

		public ExpressionToken(TExpression expression, IEntityMapper mapper)
		{
			Mapper = mapper;
			Expression = expression;
			Query = new StringBuilder();
		}

		protected override Expression VisitUnary(UnaryExpression expression) =>
			this.VisitExpression(
				new UnaryToken(expression, Mapper), 
				expression);

		protected override Expression VisitBinary(BinaryExpression expression) =>
			this.VisitExpression(
				new BinaryToken(expression, Mapper), 
				expression);

		protected override Expression VisitConstant(ConstantExpression expression) =>
			this.VisitExpression(
				new ConstantToken(expression, Mapper), 
				expression);

		protected override Expression VisitMember(MemberExpression expression) =>
			this.VisitExpression(
				new PropertyToken(expression, Mapper), 
				expression);

		protected void VisitOperation(BinaryExpression expression)
		{
			var operation = new OperationToken(expression, Mapper);
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

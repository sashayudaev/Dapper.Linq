using System.Linq.Expressions;

namespace Dapper.Linq.Core.Tokens
{
	public interface IExpressionToken<TExpression> : IToken
		where TExpression : Expression
	{
		TExpression Expression { get; }
	}
}

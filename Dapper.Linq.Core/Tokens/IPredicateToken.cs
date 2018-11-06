namespace Dapper.Linq.Core.Tokens
{
	public interface IPredicateToken : IToken
	{
		PredicateType PredicateType { get; }
	}
}

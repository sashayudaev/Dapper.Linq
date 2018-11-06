namespace Dapper.Linq.Core.Tokens
{
	public interface IToken
	{
		bool IsValid { get; }
		string Value { get; }
	}
}

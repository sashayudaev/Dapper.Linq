using Dapper.Linq.Core.Tokens;

namespace Dapper.Linq.Core.Queries
{
	public interface IQuery
	{
		IToken Token { get; }
		object Parameters { get; }
	}
}

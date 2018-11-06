using Dapper.Linq.Core;
using Dapper.Linq.Core.Tokens;

namespace Dapper.Linq.Tokens.Abstractions
{
	public abstract class TokenBase : IToken
	{
		public abstract bool IsValid { get; }
		public abstract string Value { get; }

		public TokenBase()
		{
		}

		public override string ToString() =>
			Value;
	}
}

using System.Linq.Expressions;
using Dapper.Linq.Core;
using Dapper.Linq.Core.Mappers;
using Dapper.Linq.Tokens.Abstractions;

namespace Dapper.Linq.Tokens
{
	public class FirstToken : PredicateToken
	{
		public override PredicateType PredicateType =>
			PredicateType.First;

		public override string Value =>
			" LIMIT 1";

		public FirstToken(MethodCallExpression expression, IEntityMapper mapper) 
			: base(expression, mapper)
		{
		}
	}
}

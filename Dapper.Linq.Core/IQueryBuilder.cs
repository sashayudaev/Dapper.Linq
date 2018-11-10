using System.Linq.Expressions;

namespace Dapper.Linq.Core
{
	public interface IQueryBuilder
	{
		string Build(QueryType type, Expression expression);
	}
}

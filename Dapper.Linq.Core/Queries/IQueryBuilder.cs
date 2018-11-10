using System.Linq.Expressions;

namespace Dapper.Linq.Core.Queries
{
	public interface IQueryBuilder
	{
		string Build(Expression expression);
	}
}

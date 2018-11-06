using System.Linq.Expressions;

namespace Dapper.Linq.Core
{
	public interface IQueryBuilder
	{
		string Build(Expression expression);
	}
}

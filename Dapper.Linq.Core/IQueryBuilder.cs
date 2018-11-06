using System.Linq.Expressions;

namespace Dapper.Linq.Core
{
	public interface IQueryBuilder
	{
		void Build(Expression expression);
	}
}

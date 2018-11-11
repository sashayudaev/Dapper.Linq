using System.Linq;
using System.Threading.Tasks;

namespace Dapper.Linq.Core.Queries
{
	public interface IQueryDispatcher
	{
		Task ExecuteAsync(IQuery query);
		IQueryable<TEntity> Execute<TEntity>();
	}
}

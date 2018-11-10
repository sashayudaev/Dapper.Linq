using System.Threading.Tasks;

namespace Dapper.Linq.Core.Queries
{
	public interface IQueryExecutor
	{
		Task ExecuteAsync();
	}
}

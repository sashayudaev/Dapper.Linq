using System.Threading.Tasks;

namespace Dapper.Linq.Core
{
	public interface IQueryExecutor
	{
		Task ExecuteAsync();
	}
}

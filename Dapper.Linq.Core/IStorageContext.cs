using System.Data;

namespace Dapper.Linq.Core
{
	public interface IStorageContext
	{
		IDbConnection ConfigureConnecion();
	}
}

using System.Data;
using Dapper.Linq.Core;

namespace Dapper.Linq.Context
{
	public class SybaseContext : IStorageContext
	{
		public IDbConnection ConfigureConnection() => null;
		public IDbConnection ConfigureConnection(string login, string password) => null;
	}
}

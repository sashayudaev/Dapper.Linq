using System.Data;
using System.Data.Common;
using Dapper.Linq.Core;
using Npgsql;

namespace Dapper.Linq.Context
{
	public class PostgresContext : IStorageContext
	{
		internal DbConnectionStringBuilder Builder { get; }

		public PostgresContext(string connection)
		{
			Builder = new NpgsqlConnectionStringBuilder(connection);
		}

		public IDbConnection ConfigureConnection(string login, string password)
		{
			var builder = new NpgsqlConnectionStringBuilder(
				Builder.ConnectionString)
			{
				Username = login,
				Password = password
			};
			
			return new NpgsqlConnection(builder.ConnectionString);
		}

		public IDbConnection ConfigureConnection() =>
			new NpgsqlConnection(Builder.ConnectionString);
	}
}

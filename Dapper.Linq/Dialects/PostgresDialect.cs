using Dapper.Linq.Core.Configuration;

namespace Dapper.Linq.Dialects
{
	public class PostgresDialect : SqlDialectBase
	{
		public override string GetColumnName(string prefix, string column, string alias) =>
			base.GetColumnName(null, column, alias).ToLower();
		
		public override string GetTableName(string schema, string table, string alias) =>
			base.GetTableName(schema, table, alias).ToLower();
	}
}

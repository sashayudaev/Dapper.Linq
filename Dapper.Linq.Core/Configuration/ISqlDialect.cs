namespace Dapper.Linq.Core.Configuration
{
	public interface ISqlDialect
	{
		char OpenQuote { get; }
		char CloseQuote { get; }
		char ParameterPrefix { get; }
		string BatchSeperator { get; }

		string GetTableName(string schemaName, string tableName, string alias);
		string GetColumnName(string prefix, string columnName, string alias);
		bool IsQuoted(string value);
		string QuoteString(string value);
	}
}

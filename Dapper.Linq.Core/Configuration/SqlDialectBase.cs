using System;
using System.Linq;
using System.Text;

namespace Dapper.Linq.Core.Configuration
{
	public abstract class SqlDialectBase : ISqlDialect
	{
		public virtual char OpenQuote => '"';
		public virtual char CloseQuote => '"';

		public virtual string BatchSeperator =>
			$";{Environment.NewLine}";

		public virtual char ParameterPrefix => '@';

		public virtual string GetTableName(string schema, string table, string alias)
		{
			if (String.IsNullOrWhiteSpace(table))
			{
				throw new ArgumentNullException(
					nameof(table), "tableName cannot be null or empty.");
			}

			var result = new StringBuilder();
			if (!String.IsNullOrWhiteSpace(schema))
			{
				result.AppendFormat(QuoteString(schema) + ".");
			}

			result.AppendFormat(QuoteString(table));

			if (!String.IsNullOrWhiteSpace(alias))
			{
				result.AppendFormat(" AS {0}", QuoteString(alias));
			}
			return result.ToString();
		}
		public virtual string GetColumnName(string prefix, string column, string alias)
		{
			if (String.IsNullOrWhiteSpace(column))
			{
				throw new ArgumentNullException(
					nameof(column), "columnName cannot be null or empty.");
			}

			var result = new StringBuilder();
			if (!String.IsNullOrWhiteSpace(prefix))
			{
				result.AppendFormat(QuoteString(prefix) + ".");
			}

			result.AppendFormat(QuoteString(column));

			if (!String.IsNullOrWhiteSpace(alias))
			{
				result.AppendFormat(" AS {0}", QuoteString(alias));
			}
			return result.ToString();
		}

		public virtual bool IsQuoted(string value)
		{
			if (value.Trim()[0] == OpenQuote)
			{
				return value.Trim().Last() == CloseQuote;
			}

			return false;
		}
		public virtual string QuoteString(string value)
		{
			if (IsQuoted(value) || value == "*")
			{
				return value;
			}
			return $"{OpenQuote}{value.Trim()}{CloseQuote}";
		}

		public virtual string UnQuoteString(string value) => IsQuoted(value) 
			? value.Substring(1, value.Length - 2) 
			: value;
	}
}

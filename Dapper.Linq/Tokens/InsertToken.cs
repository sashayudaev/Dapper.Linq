using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Dapper.Linq.Core.Mappers;
using Dapper.Linq.Tokens.Abstractions;

namespace Dapper.Linq.Tokens
{
	public class InsertToken : ExpressionToken<ConstantExpression>
	{
		public IPropertyMapper[] Properties { get; }

		public string Schema =>
			Mapper.SchemaName;

		public string Table =>
			Mapper.TableName;

		public string Columns
		{
			get
			{

				var columns = new StringBuilder();
				foreach (var property in Properties)
				{
					columns.Append($"{property.ColumnName}, ");
				}

				return columns.Remove(columns.Length - 2, 2).ToString();
			}
		}

		public string Parameters
		{
			get
			{
				var parameters = new StringBuilder();
				foreach (var property in Properties)
				{
					parameters.Append($"@{property.ColumnName}, ");
				}

				return parameters.Remove(parameters.Length - 2, 2).ToString();
			}
		}

		public InsertToken(ConstantExpression expression, IEntityMapper mapper) 
			: base(expression, mapper)
		{
			Properties = Mapper
				.EntityType.GetProperties()
				.Select(GetPropertyMap)
				.ToArray();
		}

		public override string Value =>
			$"INSERT INTO {Schema}.{Table} ({Columns}) VALUES ({Parameters})";

		private IPropertyMapper GetPropertyMap(PropertyInfo property) =>
			Mapper.GetProperty(property.Name);
	}
}

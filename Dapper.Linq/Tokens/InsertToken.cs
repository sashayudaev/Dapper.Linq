using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Dapper.Linq.Core;
using Dapper.Linq.Core.Mappers;
using Dapper.Linq.Tokens.Abstractions;

namespace Dapper.Linq.Tokens
{
	public class InsertToken : PredicateToken
	{
		public override PredicateType PredicateType =>
			PredicateType.Insert;

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

				return columns.Remove(columns.Length - 2, 1).ToString();
			}
		}

		public InsertToken(MethodCallExpression expression, IEntityMapper mapper) 
			: base(expression, mapper)
		{
			Properties = Mapper
				.EntityType.GetProperties()
				.Select(GetPropertyMap)
				.ToArray();
		}

		public override string Value =>
			$"INSERT INTO {Schema}.{Table} ({Columns}) VALUES ";

		private IPropertyMapper GetPropertyMap(PropertyInfo property) =>
			Mapper.GetProperty(property.Name);
	}
}

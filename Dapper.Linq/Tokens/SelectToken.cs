using System.Linq;
using System.Reflection;
using System.Text;
using Dapper.Linq.Core;
using Dapper.Linq.Core.Mappers;
using Dapper.Linq.Core.Tokens;
using Dapper.Linq.Tokens.Abstractions;

namespace Dapper.Linq.Tokens
{
	public class SelectToken : TokenBase, IPredicateToken
	{
		public override bool IsValid =>
			Mapper.EntityType != null;

		public PredicateType PredicateType =>
			PredicateType.Select;

		public IEntityMapper Mapper { get; }
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

		public override string Value =>
			$"SELECT {Columns} FROM {Schema}.{Table}";

		public SelectToken(IEntityMapper mapper)
		{
			Mapper = mapper;
			Properties = Mapper
				.EntityType.GetProperties()
				.Select(GetPropertyMap)
				.ToArray();
		}

		private IPropertyMapper GetPropertyMap(PropertyInfo property) =>
			Mapper.GetProperty(property.Name);
	}
}

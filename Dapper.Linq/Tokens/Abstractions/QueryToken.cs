using System;
using System.Linq;
using System.Reflection;
using Dapper.Linq.Core.Mappers;

namespace Dapper.Linq.Tokens.Abstractions
{
	public abstract class QueryToken : TokenBase
	{
		public IEntityMapper Mapper { get; }
		public IPropertyMapper[] Properties { get; }

		public override bool IsValid => true;

		public string Schema =>
			Mapper.SchemaName;

		public string Table =>
			Mapper.TableName;

		public QueryToken(IEntityMapper mapper)
		{
			Mapper = mapper ??
				throw new ArgumentNullException(nameof(mapper));

			Properties = Mapper
				.EntityType.GetProperties()
				.Select(GetPropertyMap)
				.ToArray();
		}

		protected IPropertyMapper GetPropertyMap(PropertyInfo property) =>
			Mapper.GetProperty(property.Name);
	}
}

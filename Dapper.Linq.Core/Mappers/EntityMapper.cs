using System;
using System.Collections.Generic;
using System.Reflection;

namespace Dapper.Linq.Core.Mappers
{
	public class EntityMapper<TEntity> : IEntityMapper<TEntity>
		where TEntity : class
	{
		public Type EntityType { get; }

		public string TableName { get; private set; }
		public string SchemaName { get; private set; }

		public IDictionary<string, IPropertyMapper> Properties { get; }

		public EntityMapper()
		{
			EntityType = typeof(TEntity);
			Properties = new Dictionary<string, IPropertyMapper>();
		}

		public void Table(string table)
		{
			TableName = table ??
				throw new ArgumentNullException(nameof(table));
		}

		public void Schema(string schema)
		{
			SchemaName = schema ??
				throw new ArgumentNullException(nameof(schema));
		}

		public virtual IPropertyMapper Map(PropertyInfo property)
		{
			var propertyMapper = new PropertyMapper(property);
			Properties.Add(property.Name, propertyMapper);

			return propertyMapper;
		}

		public IPropertyMapper GetProperty(string name) =>
			Properties[name];
	}
}

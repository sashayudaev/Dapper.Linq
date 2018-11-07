using System.Reflection;
using Dapper.Linq.Helpers;
using Dapper.Linq.Core.Mappers;

namespace Dapper.Linq.Mappers
{
	using Table = Core.Attributes.TableAttribute;
	using Key = Core.Attributes.KeyAttribute;
	using CompositeKey = Core.Attributes.CompositeKeyAttribute;
	using Column = Core.Attributes.ColumnAttribute;

	public class AttributeEntityMapper<TEntity> : EntityMapper<TEntity>
		where TEntity : class
	{
		public AttributeEntityMapper()
		{
			this.MapEntity();
		}

		private void MapEntity()
		{
			if (EntityType.HasAttribute(out Table table))
			{
				Table(table.Name);
				Schema(table.Schema);
			}

			foreach (var property in EntityType.GetProperties())
			{
				this.MapProperty(property);
			}
		}

		private void MapProperty(PropertyInfo property)
		{
			var map = Map(property);
			if (property.HasAttribute(out Key _))
			{
				map.Key(KeyType.Identity);
			}

			if (property.HasAttribute(out CompositeKey _))
			{
				map.Key(KeyType.Assigned);
			}

			if (property.HasAttribute(out Column column))
			{
				map.Column(column.Name);
			}
		}
	}
}

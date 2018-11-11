using System;
using System.Reflection;

namespace Dapper.Linq.Core.Mappers
{
	public class PropertyMapper : IPropertyMapper
	{
		public PropertyInfo Property { get; }

		public KeyType KeyType { get; private set; }
		public string Name { get; private set; }
		public string ColumnName { get; private set; }

		public PropertyMapper(PropertyInfo property)
		{
			Property = property;
		}

		public void Key(KeyType key)
		{
			KeyType = key;
		}
		public void Column(string column)
		{
			ColumnName = column ??
				throw new ArgumentNullException(nameof(column));
		}
	}
}

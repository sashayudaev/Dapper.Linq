using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Linq.Core.Mappers
{
	public class PropertyMapper : IPropertyMapper
	{
		public KeyType KeyType { get; private set; }
		public string Name { get; private set; }
		public string ColumnName { get; private set; }
		public PropertyInfo Property { get; }

		public PropertyMapper(PropertyInfo property)
		{
			Property = property;
		}

		public void Column(string column)
		{
			ColumnName = column ??
				throw new ArgumentNullException(nameof(column));
		}

		public void Key(KeyType key)
		{
			KeyType = key;
		}
	}
}

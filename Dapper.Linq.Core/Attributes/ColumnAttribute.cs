using System;

namespace Dapper.Linq.Core.Attributes
{
	public class ColumnAttribute : Attribute
	{
		public string Name { get; }

		public ColumnAttribute(string name)
		{
			Name = name ??
				throw new ArgumentNullException(name);
		}
	}
}

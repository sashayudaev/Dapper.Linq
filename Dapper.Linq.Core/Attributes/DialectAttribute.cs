using System;

namespace Dapper.Linq.Core.Attributes
{
	public class DialectAttribute : Attribute
	{
		public StorageType Dialect { get; }

		public DialectAttribute(StorageType dialect)
		{
			Dialect = dialect;
		}
	}
}

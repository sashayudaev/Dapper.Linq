using System;
using Dapper.Linq.Core;

namespace Dapper.Linq.Attributes
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

using System;
using Dapper.Linq.Core.Configuration;

namespace Dapper.Linq.Configuration
{
	public class DapperConfiguration : IDapperConfiguration
	{
		public static Type EntityMapper { get; private set; }
		public static ISqlDialect Dialect { get; private set; }

		public static IDapperConfiguration Create() =>
			new DapperConfiguration();

		public IDapperConfiguration UseMapper(Type mapper)
		{
			EntityMapper = mapper;
			return this;
		}
		public IDapperConfiguration UseDialect(ISqlDialect dialect)
		{
			Dialect = dialect;
			return this;
		}
	}
}

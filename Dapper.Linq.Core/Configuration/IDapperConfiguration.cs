using System;

namespace Dapper.Linq.Core.Configuration
{
	public interface IDapperConfiguration
	{
		ISqlDialect Dialect { get; }

		IDapperConfiguration UseMapper(Type mapper);
		IDapperConfiguration UseDialect(ISqlDialect dialect);
	}
}

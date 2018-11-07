using System;

namespace Dapper.Linq.Core.Configuration
{
	public interface IDapperConfiguration
	{
		IDapperConfiguration UseMapper(Type mapper);
		IDapperConfiguration UseDialect(ISqlDialect dialect);
	}
}

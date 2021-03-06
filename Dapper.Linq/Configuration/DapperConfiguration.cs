﻿using System;
using Dapper.Linq.Mappers;
using Dapper.Linq.Core.Configuration;
using Dapper.Linq.Core.Mappers;

namespace Dapper.Linq.Configuration
{
	public static class DapperConfigurationExtensions
	{
		public static IDapperConfiguration UseMapper<TMapper>(this IDapperConfiguration configuration)
			where TMapper : IEntityMapper =>
			configuration.UseMapper(typeof(TMapper));
	}

	public class DapperConfiguration : IDapperConfiguration
	{
		public static Type EntityMapper { get; private set; }
		public ISqlDialect Dialect { get; private set; }

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
		public static IEntityMapper GetMapper<TEntity>() =>
			EntityMappers.GetOrCreate(typeof(TEntity));
		public static IEntityMapper GetMapper(Type entity) =>
			EntityMappers.GetOrCreate(entity);

		private static readonly EntityMapperCollection EntityMappers =
			new EntityMapperCollection();
	}
}

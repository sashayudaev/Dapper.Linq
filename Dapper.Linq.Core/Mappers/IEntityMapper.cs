﻿using System;
using System.Collections.Generic;

namespace Dapper.Linq.Core.Mappers
{
	public interface IEntityMapper
	{
		Type EntityType { get; }
		string TableName { get; }
		string SchemaName { get; }

		IPropertyMapper GetProperty(string name);
	}

	public interface IEntityMapper<TEntity> : IEntityMapper
		where TEntity : class
	{

	}
}
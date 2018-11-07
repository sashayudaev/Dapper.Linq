using System;

namespace Dapper.Linq.Core.Mappers
{
	public interface IEntityMapper
	{
		Type EntityType { get; }
		string TableName { get; }
		string SchemaName { get; }
	}

	public interface IEntityMapper<TEntity> : IEntityMapper
		where TEntity : class
	{

	}
}

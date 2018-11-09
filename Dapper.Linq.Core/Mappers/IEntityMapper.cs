using System;

namespace Dapper.Linq.Core.Mappers
{
	public interface IEntityMapper
	{
		Type EntityType { get; }
		string TableName { get; }
		string SchemaName { get; }

		IPropertyMapper GetProperty(string name);
	}
}

using System;
using Dapper.Linq.Core.Configuration;

namespace Dapper.Linq.Configuration
{
	public class AttributeEntityMapper<TEntity> : IEntityMapper<TEntity>
		where TEntity : class
	{
		public Type EntityType { get; }

		public string Table { get; private set; }
		public string Schema { get; private set; }

		public AttributeEntityMapper()
		{
			EntityType = typeof(TEntity);
		}
	}
}

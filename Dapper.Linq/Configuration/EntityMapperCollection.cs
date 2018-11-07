using System;
using System.Collections.Concurrent;
using Dapper.Linq.Core.Configuration;

namespace Dapper.Linq.Configuration
{
	public class EntityMapperCollection : ConcurrentDictionary<Type, IEntityMapper>
	{
		public IEntityMapper GetOrCreate(Type entity)
		{
			if(!this.ContainsKey(entity))
			{
				this.Add(entity);
			}

			return this[entity];
		}

		public void Add(Type entity)
		{
			var mapperType = DapperConfiguration
				.EntityMapper
				.MakeGenericType(entity);

			var mapper = (IEntityMapper) Activator.CreateInstance(mapperType);
			this.TryAdd(entity, mapper);
		}
	}
}

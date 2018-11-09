using System;
using System.Collections.Concurrent;
using Dapper.Linq.Configuration;
using Dapper.Linq.Core.Mappers;

namespace Dapper.Linq.Mappers
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
			var mapperType = DapperConfiguration.EntityMapper;

			var mapper = Activator.CreateInstance(mapperType, entity)
				as IEntityMapper;

			this.TryAdd(entity, mapper);
		}
	}
}

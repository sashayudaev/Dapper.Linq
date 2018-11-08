using System;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Linq.Core;

namespace Dapper.Linq
{
	public class Storage : ICrudStorage, IQueryStorage
	{
		public IQueryFactory Factory { get; }

		public Storage(IQueryFactory factory)
		{
			Factory = factory;
		}

		#region ICrudStorage
		public IQueryable<TEntity> Select<TEntity>()
			where TEntity : class => Factory
			.CreateProvider<TEntity>()
			.CreateQuery<TEntity>(null);

		public Task InsertAsync<TEntity>(TEntity entity) 
			where TEntity : class
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync<TEntity>(TEntity entity) 
			where TEntity : class
		{
			throw new NotImplementedException();
		}

		public Task DeleteAsync<TEntity>(TEntity entity) 
			where TEntity : class
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}

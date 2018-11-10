using System.Linq;
using System.Threading.Tasks;
using Dapper.Linq.Core;
using Dapper.Linq.Core.Queries;
using Dapper.Linq.Queries;

namespace Dapper.Linq
{
	public class Storage : ICrudStorage, IQueryStorage
	{
		public IStorageContext Context { get; }
		public IQueryDispatcher QueryDispatcher { get; }

		public Storage(IStorageContext context)
		{
			Context = context;
			QueryDispatcher = new QueryDispatcher(Context);
		}

		#region ICrudStorage
		public IQueryable<TEntity> Select<TEntity>()
			where TEntity : class =>
			QueryDispatcher.Execute<TEntity>();

		public Task InsertAsync<TEntity>(TEntity entity) 
			where TEntity : class
		{
			var query = new InsertQuery<TEntity>(entity);
			return QueryDispatcher.ExecuteAsync(query);
		}

		public Task UpdateAsync<TEntity>(TEntity entity) 
			where TEntity : class
		{
			var query = new UpdateQuery<TEntity>(entity);
			return QueryDispatcher.ExecuteAsync(query);
		}

		public Task DeleteAsync<TEntity>(TEntity entity) 
			where TEntity : class
		{
			var query = new DeleteQuery<TEntity>(entity);
			return QueryDispatcher.ExecuteAsync(query);
		}
		#endregion
	}
}

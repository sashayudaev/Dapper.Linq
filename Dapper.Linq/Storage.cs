using System;
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
			:this(context, CreateDispatcher(context))
		{

		}

		public Storage(IStorageContext context, IQueryDispatcher dispatcher)
		{
			Context = context ??
				throw new ArgumentNullException(nameof(context));
			QueryDispatcher = dispatcher ??
				throw new ArgumentNullException(nameof(dispatcher));
		}

		#region ICrudStorage
		public IQueryable<TEntity> Select<TEntity>()
			where TEntity : class =>
			QueryDispatcher.Execute<TEntity>();

		public async Task InsertAsync<TEntity>(TEntity entity) 
			where TEntity : class
		{
			var query = new InsertQuery<TEntity>(entity);
			await QueryDispatcher.ExecuteAsync(query);
		}

		public async Task UpdateAsync<TEntity>(TEntity entity) 
			where TEntity : class
		{
			var query = new UpdateQuery<TEntity>(entity);
			await QueryDispatcher.ExecuteAsync(query);
		}

		public async Task DeleteAsync<TEntity>(TEntity entity) 
			where TEntity : class
		{
			var query = new DeleteQuery<TEntity>(entity);
			await QueryDispatcher.ExecuteAsync(query);
		}
		#endregion

		private static IQueryDispatcher CreateDispatcher(
			IStorageContext context) => new QueryDispatcher(context);
	}
}

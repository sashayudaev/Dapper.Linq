using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dapper.Linq.Core;
using Dapper.Linq.Queries;

namespace Dapper.Linq
{
	public class Storage : ICrudStorage, IQueryStorage
	{
		public IStorageContext Context { get; }

		public Storage(IStorageContext context)
		{
			Context = context;
		}

		#region ICrudStorage
		public IQueryable<TEntity> Select<TEntity>()
			where TEntity : class
		{
			var method = Expression.Call(
				Expression.Constant(this),
				new Func<IQueryable<TEntity>>(this.Select<TEntity>).Method);

			var provider = new QueryProvider<TEntity>(Context);
			return provider.CreateQuery<TEntity>(method);
		}

		public Task InsertAsync<TEntity>(TEntity entity) 
			where TEntity : class
		{
			return new InsertQueryExecutor<TEntity>(Context, entity).ExecuteAsync();
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

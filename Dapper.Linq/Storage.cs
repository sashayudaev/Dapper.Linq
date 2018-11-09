using System;
using System.Linq;
using System.Linq.Expressions;
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
			where TEntity : class
		{
			var info = typeof(ICrudStorage).GetMethod("Select");

			var method = Expression.Call(
				Expression.Constant(this),
				new Func<IQueryable<TEntity>>(this.Select<TEntity>).Method);

			return Factory.CreateQuery<TEntity>(method);
		}

		public Task InsertAsync<TEntity>(TEntity entity) 
			where TEntity : class
		{
			var parameter = Expression.Parameter(typeof(TEntity));

			var info = typeof(ICrudStorage).GetMethod("InsertAsync");

			var method = Expression.Call(
				Expression.Constant(this),
				new Func<TEntity, Task>(this.InsertAsync).Method, 
				Expression.Constant(entity));

			var result = Factory.CreateProvider<TEntity>().Execute<TEntity>(method);
			return null;
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

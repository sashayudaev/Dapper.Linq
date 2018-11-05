using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Linq.Core;

namespace Dapper.Linq
{
	public class Storage : ICrudStorage, IQueryStorage
	{
		public IQueryProvider Provider { get; }
		public IStorageContext Context { get; }
		public IDbConnection Connection { get; }

		public Storage(IQueryProvider provider, IStorageContext context)
		{
			Provider = provider ?? 
				throw new ArgumentNullException(nameof(provider));
			Context = context ??
				throw new ArgumentNullException(nameof(context));

			Connection = Context.ConfigureConnection();
		}

		#region ICrudStorage
		public IQueryable<TEntity> Select<TEntity>()
			where TEntity : class =>
			Provider.CreateQuery<TEntity>(null);

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

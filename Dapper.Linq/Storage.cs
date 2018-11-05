using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dapper.Linq.Core;

namespace Dapper.Linq
{
	public class Storage : ICrudStorage, IQueryStorage
	{
		public IQueryProvider Provider { get; }

		public Storage(IQueryProvider provider)
		{
			Provider = provider ?? 
				throw new ArgumentNullException(nameof(provider));
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Linq.Core;

namespace Dapper.Linq
{
	public class Storage : ICrudStorage, IQueryStorage
	{
		public IStorageContext Context { get; }
		public IDbConnection Connection { get; }

		public Storage(IStorageContext context)
		{
			Context = context ??
				throw new ArgumentNullException(nameof(context));

			Connection = Context.ConfigureConnecion();
		}

		#region ICrudStorage
		public IQueryable<TEntity> Select<TEntity>() 
			where TEntity : class
		{
			throw new NotImplementedException();
		}

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

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Dapper.Linq.Core
{
	public interface ICrudStorage
	{
		IQueryable<TEntity> Select<TEntity>()
			where TEntity : class;
		Task InsertAsync<TEntity>(TEntity entity)
			where TEntity : class;
		Task UpdateAsync<TEntity>(TEntity entity)
			where TEntity : class;
		Task DeleteAsync<TEntity>(TEntity entity)
			where TEntity : class;
	}
}

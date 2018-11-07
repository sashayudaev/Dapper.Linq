using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Linq.Core.Configuration
{
	public interface IEntityMapper
	{
		Type EntityType { get; }
		string Table { get; }
		string Schema { get; }
	}

	public interface IEntityMapper<TEntity> : IEntityMapper
		where TEntity : class
	{

	}
}

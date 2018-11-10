using System.Linq;
using System.Reflection;
using Dapper.Linq.Configuration;
using Dapper.Linq.Core.Mappers;
using Dapper.Linq.Core.Queries;
using Dapper.Linq.Core.Tokens;

namespace Dapper.Linq.Queries
{
	public abstract class QueryBase<TEntity> : IQuery
	{
		public TEntity Entity { get; }
		public IEntityMapper Mapper { get; }
		public IToken Token { get; protected set; }
		public object Parameters { get; protected set; }

		public QueryBase(TEntity entity)
		{
			Mapper = DapperConfiguration.GetMapper<TEntity>();

			Entity = entity;
		}

		protected abstract IToken CreateToken();

		protected object CreateParameters(object entity)
		{
			var values = entity.GetType()
				.GetProperties()
				.ToDictionary(GetName, GetValue);

			var parameters = new DynamicParameters();
			foreach (var value in values)
			{
				parameters.Add(value.Key, value.Value);
			}
			return parameters;
		}

		private string GetName(PropertyInfo info) =>
			Mapper.GetProperty(info.Name).ColumnName;

		private object GetValue(PropertyInfo info) =>
			info.GetValue(Entity);
	}
}

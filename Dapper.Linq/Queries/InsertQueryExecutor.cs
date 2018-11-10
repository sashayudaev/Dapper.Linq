using System.Linq.Expressions;
using System.Threading.Tasks;
using Dapper.Linq.Configuration;
using Dapper.Linq.Core;
using Dapper.Linq.Core.Mappers;
using Dapper.Linq.Core.Queries;

namespace Dapper.Linq.Queries
{
	public class InsertQueryExecutor<TEntity> : IQueryExecutor
	{
		public TEntity Entity { get; }
		public IStorageContext Context { get; }

		public InsertQueryExecutor(IStorageContext context, TEntity entity)
		{
			Entity = entity;
			Context = context;
		}

		public Task ExecuteAsync()
		{
			var mapper = DapperConfiguration.GetMapper<TEntity>();
			var queryBuilder = new QueryBuilder(mapper);
			var expression = Expression.Constant(Entity);

			var sql = queryBuilder.Build(expression);
			var parameters = this.GenerateParameters(mapper);
			return this.Query(sql, parameters);
		}

		private DynamicParameters GenerateParameters(IEntityMapper mapper)
		{
			var parameters = new DynamicParameters();
			foreach (var property in Entity.GetType().GetProperties())
			{
				var map = mapper.GetProperty(property.Name);
				parameters.Add(map.ColumnName, property.GetValue(Entity));
			}

			return parameters;
		}

		private async Task Query(string query, object parameters)
		{
			using (var connection = Context.ConfigureConnection())
			{
				connection.Open();
				await connection.ExecuteAsync(query, parameters, commandType: System.Data.CommandType.Text);
				return;
			}
		}
	}
}

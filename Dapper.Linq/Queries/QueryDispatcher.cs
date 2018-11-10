using System.Linq;
using System.Threading.Tasks;
using Dapper.Linq.Core;
using Dapper.Linq.Core.Queries;

namespace Dapper.Linq.Queries
{
	public class QueryDispatcher : IQueryDispatcher
	{
		public IStorageContext Context { get; }

		public QueryDispatcher(IStorageContext context)
		{
			Context = context;
		}

		public IQueryable<TEntity> Execute<TEntity>() => this
			.CreateProvider<TEntity>()
			.CreateQuery<TEntity>(null);

		public async Task ExecuteAsync(IQuery query)
		{
			using (var connection = Context.ConfigureConnection())
			{
				//connection.Open();
				var id = await connection.ExecuteAsync(
					query.Token.Value,
					query.Parameters);
			}
		}

		private IQueryProvider CreateProvider<TEntity>() =>
			new QueryProvider<TEntity>(Context);
	}
}

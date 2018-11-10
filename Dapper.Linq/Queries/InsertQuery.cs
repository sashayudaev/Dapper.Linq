using Dapper.Linq.Tokens;
using Dapper.Linq.Core.Tokens;

namespace Dapper.Linq.Queries
{
	public class InsertQuery<TEntity> : QueryBase<TEntity>
	{
		public InsertQuery(TEntity entity)
			:base(entity)
		{
			Token = this.CreateToken();
			Parameters = this.CreateParameters(entity);
		}

		protected override IToken CreateToken() =>
			new InsertToken(Mapper);
	}
}

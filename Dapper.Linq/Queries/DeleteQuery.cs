using Dapper.Linq.Tokens;
using Dapper.Linq.Core.Tokens;

namespace Dapper.Linq.Queries
{
	public class DeleteQuery<TEntity> : QueryBase<TEntity>
	{
		public DeleteQuery(TEntity entity)
			:base(entity)
		{
			Token = this.CreateToken();
			Parameters = this.CreateParameters(entity);
		}

		protected override IToken CreateToken() =>
			new DeleteToken(Mapper);
	}
}

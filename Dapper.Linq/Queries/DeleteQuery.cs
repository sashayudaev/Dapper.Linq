using System;
using Dapper.Linq.Tokens;
using Dapper.Linq.Core.Tokens;
using System.Linq.Expressions;
using Dapper.Linq.Core.Queries;

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

		public static Expression<Func<TEntity, IQuery>> Create =
			entity => new DeleteQuery<TEntity>(entity);

		protected override IToken CreateToken() =>
			new DeleteToken(Mapper);
	}
}

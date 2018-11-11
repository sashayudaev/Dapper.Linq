using Dapper.Linq.Tokens;
using Dapper.Linq.Core.Tokens;
using System.Linq.Expressions;
using System;
using Dapper.Linq.Core.Queries;

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


		public static Expression<Func<TEntity, IQuery>> Create =
			entity => new InsertQuery<TEntity>(entity);

		protected override IToken CreateToken() =>
			new InsertToken(Mapper);
	}
}

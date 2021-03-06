﻿using System;
using System.Linq.Expressions;
using Dapper.Linq.Core.Queries;
using Dapper.Linq.Core.Tokens;
using Dapper.Linq.Tokens.Queries;

namespace Dapper.Linq.Queries
{
	public class UpdateQuery<TEntity> : QueryBase<TEntity>
	{
		public UpdateQuery(TEntity entity) 
			: base(entity)
		{
			Token = this.CreateToken();
			Parameters = this.CreateParameters(entity);
		}

		public static Expression<Func<TEntity, IQuery>> Create =
			entity => new UpdateQuery<TEntity>(entity);

		protected override IToken CreateToken() =>
			new UpdateToken(Entity, Mapper);
	}
}

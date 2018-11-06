using System;
using Dapper.Linq.Core;
using Dapper.Linq.Core.Tokens;
using Dapper.Linq.Tokens.Abstractions;

namespace Dapper.Linq.Tokens
{
	public class SelectToken : TokenBase, IPredicateToken
	{
		public PredicateType PredicateType =>
			PredicateType.Select;

		public override bool IsValid =>
			EntityType != null;

		public string Schema =>
			"public";

		public string Table =>
			EntityType.Name;

		public string Columns =>
			"*";

		public Type EntityType { get; }

		public override string Value =>
			$"SELECT {Columns} FROM {Schema}.{Table}";

		public SelectToken(Type entity)
		{
			EntityType = entity;
		}
	}
}

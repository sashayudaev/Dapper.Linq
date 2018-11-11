using System.Collections.Generic;
using System.Text;
using Dapper.Linq.Core;
using Dapper.Linq.Core.Tokens;

namespace Dapper.Linq.Tokens
{
	public class PredicateCollection : SortedList<PredicateType, IPredicateToken>, IToken
	{
		public bool IsValid => 
			true;

		public string Value =>
			this.BuildQuery();

		public PredicateCollection()
		{

		}

		public PredicateCollection(IComparer<PredicateType> comparer)
			:base(comparer)
		{
				
		}

		public void Add(IPredicateToken predicate)
		{
			var key = predicate.PredicateType;
			if(!this.ContainsKey(key))
			{
				this.Add(key, predicate);
			}
		}

		public string BuildQuery()
		{
			var query = new StringBuilder();

			foreach (var token in Values)
			{
				query.Append(token.Value);
			}

			return query.ToString();
		}
	}
}

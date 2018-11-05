using System.Collections.Generic;
using System.Text;
using Dapper.Linq.Core;

namespace Dapper.Linq.Predicates
{
	public class PredicateCollection : SortedList<PredicateType, IPredicate>, IPredicate
	{
		public PredicateType PredicateType =>
			throw new KeyNotFoundException();

		public PredicateCollection()
		{

		}

		public PredicateCollection(IComparer<PredicateType> comparer)
			:base(comparer)
		{
				
		}

		public void Add(IPredicate predicate) =>
			this.Add(predicate.PredicateType, predicate);

		public string BuildQuery() =>
			this.BuildQuery(new StringBuilder());

		public string BuildQuery(StringBuilder query)
		{
			foreach (var predicate in Values)
			{
				var updated = predicate.BuildQuery(query);
				query.Append(updated);
			}

			return query.ToString();
		}
	}
}

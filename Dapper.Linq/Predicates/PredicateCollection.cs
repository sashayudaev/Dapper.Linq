using System.Collections.Generic;
using System.Text;
using Dapper.Linq.Core;

namespace Dapper.Linq.Predicates
{
	public class PredicateCollection : SortedList<PredicateType, IPredicate>, IPredicate
	{
		private bool HasSelectQuery =>
			this.TryGetValue(PredicateType.Select, out _);

		public PredicateType PredicateType { get; } =
			PredicateType.Select;

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
			if(!HasSelectQuery)
			{
				query.Append("SELECT * FROM Table");
			}

			foreach (var predicate in Values)
			{
				var updated = predicate.BuildQuery(query);
				query.Append(updated);
			}

			return query.ToString();
		}
	}
}

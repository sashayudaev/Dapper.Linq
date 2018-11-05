using System.Text;

namespace Dapper.Linq.Core
{
	public interface IPredicate
	{
		PredicateType PredicateType { get; }
		string BuildQuery(StringBuilder query);
	}
}

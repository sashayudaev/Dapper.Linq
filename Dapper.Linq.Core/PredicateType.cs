using System.ComponentModel;

namespace Dapper.Linq.Core
{
	public enum PredicateType
	{
		[Description("Select")]
		Select,

		[Description("Where")]
		Where,

		[Description("GroupBy")]
		GroupBy,

		[Description("OrderBy")]
		OrderBy,

		[Description("OrderByDescending")]
		OrderByDescending,

		[Description("Take")]
		Take
	}
}

using System.ComponentModel;

namespace Dapper.Linq.Core
{
	public enum PredicateType
	{
		[Description("Select")]
		Select,

		[Description("InsertAsync")]
		Insert,

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

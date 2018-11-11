using System.ComponentModel;

namespace Dapper.Linq.Core
{
	public enum PredicateType
	{
		//TODO: Remove
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
		Take,

		[Description("FirstOrDefault")]
		First
	}
}

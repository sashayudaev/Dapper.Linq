using Dapper.Linq.Core.Attributes;

namespace Dapper.Linq.Tests.Stubs
{
	[Table("table", Schema = "schema")]
	public class FakeEntity
	{
		[Key]
		[Column("id")]
		public int Id { get; set; }

		[Column("name")]
		public string Name { get; set; }

		[Column("valid")]
		public bool IsValid { get; set; }
	}
}

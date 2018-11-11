using System.Linq;
using System.Text;
using Dapper.Linq.Core.Mappers;
using Dapper.Linq.Helpers;
using Dapper.Linq.Tokens.Abstractions;

namespace Dapper.Linq.Tokens
{
	public class InsertToken : QueryToken
	{
		public override bool IsValid => 
			true;

		public override string Value =>
			$"INSERT INTO {Schema}.{Table} ({Columns}) VALUES ({Parameters})";

		public string Columns =>
			this.CreateColumnNames();

		public string Parameters =>
			this.CreateColumnNames("@");

		public InsertToken(IEntityMapper mapper)
			:base(mapper)
		{
		}

		private string CreateColumnNames(string prefix = "")
		{
			var columns = new StringBuilder();
			var names = Properties
				.Select(property => property.ColumnName)
				.Select(name => $"{prefix}{name}, ");

			columns.AppendRange(names).RemoveLast(2);
			return columns.ToString();
		}
	}
}

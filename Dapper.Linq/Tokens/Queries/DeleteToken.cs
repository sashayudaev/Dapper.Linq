using System.Linq;
using System.Text;
using Dapper.Linq.Core.Mappers;
using Dapper.Linq.Helpers;
using Dapper.Linq.Tokens.Abstractions;

namespace Dapper.Linq.Tokens
{
	public class DeleteToken : QueryToken
	{
		public override bool IsValid => 
			true;

		public override string Value =>
			$"DELETE FROM {Schema}.{Table} WHERE ({Where})";

		public string Where
		{
			get
			{
				var columns = new StringBuilder();

				var names = Properties
					.Select(property => property.ColumnName)
					.Select(name => $"{name} = @{name} AND ");
		
				columns.AppendRange(names).RemoveLast(5);
				return columns.ToString();
			}
		}

		public DeleteToken(IEntityMapper mapper)
			:base(mapper)
		{
		
		}
	}
}

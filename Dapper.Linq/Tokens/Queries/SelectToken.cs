using System.Linq;
using System.Text;
using Dapper.Linq.Core;
using Dapper.Linq.Core.Mappers;
using Dapper.Linq.Core.Tokens;
using Dapper.Linq.Helpers;
using Dapper.Linq.Tokens.Abstractions;

namespace Dapper.Linq.Tokens
{
	public class SelectToken : QueryToken, IPredicateToken
	{
		public PredicateType PredicateType =>
			PredicateType.Select;

		public override bool IsValid =>
			Mapper.EntityType != null;

		public string Columns
		{
			get
			{
				var columns = new StringBuilder();
				var names = Properties
					.Select(property => property.ColumnName)
					.Select(name => $"{name}, ");

				columns.AppendRange(names).RemoveLast(2);
				return columns.ToString();
			}
		}

		public override string Value =>
			$"SELECT {Columns} FROM {Schema}.{Table}";

		public SelectToken(IEntityMapper mapper)
			: base(mapper)
		{
		}
	}
}

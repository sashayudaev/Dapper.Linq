using System.Linq;
using System.Text;
using Dapper.Linq.Core.Mappers;
using Dapper.Linq.Helpers;
using Dapper.Linq.Tokens.Abstractions;

namespace Dapper.Linq.Tokens.Queries
{
	public class UpdateToken : QueryToken
	{
		public object Entity { get; }

		public override string Value =>
			$"UPDATE {Schema}.{Table} SET {Columns} WHERE ({Where})";

		public string Columns
		{
			get
			{
				var columns = new StringBuilder();

				var names = Properties
					.Select(property => property.ColumnName)
					.Select(name => $"{name} = @{name}, ");

				columns.AppendRange(names).RemoveLast(2);
				return columns.ToString();
			}
		}

		public string Where
		{
			get
			{
				var id = Properties.FirstOrDefault(p => p.KeyType == KeyType.Identity);
				var value = id.Property.GetValue(Entity);
				return $"{id.ColumnName} = {value}";
			}
		}

		public UpdateToken(object entity, IEntityMapper mapper) 
			: base(mapper)
		{
			Entity = entity;
		}
	}
}

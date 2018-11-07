using System.Reflection;

namespace Dapper.Linq.Core.Mappers
{
	public interface IPropertyMapper
	{
		KeyType KeyType { get; }
		string Name { get; }
		string ColumnName { get; }
		PropertyInfo Property { get; }

		void Key(KeyType key);
		void Column(string column);
	}
}

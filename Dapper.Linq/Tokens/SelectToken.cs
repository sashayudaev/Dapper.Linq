﻿using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Dapper.Linq.Core;
using Dapper.Linq.Core.Mappers;
using Dapper.Linq.Tokens.Abstractions;

namespace Dapper.Linq.Tokens
{
	public class SelectToken : PredicateToken
	{
		public override bool IsValid =>
			Mapper.EntityType != null;

		public override PredicateType PredicateType =>
			PredicateType.Select;

		public IPropertyMapper[] Properties { get; }

		public string Schema =>
			Mapper.SchemaName;

		public string Table =>
			Mapper.TableName;

		public string Columns
		{
			get
			{
				
				var columns = new StringBuilder();
				foreach (var property in Properties)
				{
					columns.Append($"{property.ColumnName}, ");
				}

				return columns.Remove(columns.Length - 2, 1).ToString();
			}
		}

		public override string Value =>
			$"SELECT {Columns} FROM {Schema}.{Table}";

		public SelectToken(MethodCallExpression expression, IEntityMapper mapper)
			: base(expression, mapper)
		{
			Properties = Mapper
				.EntityType.GetProperties()
				.Select(GetPropertyMap)
				.ToArray();
		}

		private IPropertyMapper GetPropertyMap(PropertyInfo property) =>
			Mapper.GetProperty(property.Name);
	}
}

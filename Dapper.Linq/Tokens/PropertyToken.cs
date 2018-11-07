using System;
using System.Linq.Expressions;
using Dapper.Linq.Core.Mappers;
using Dapper.Linq.Tokens.Abstractions;

namespace Dapper.Linq.Tokens
{
	public class PropertyToken : ExpressionToken<MemberExpression>
	{
		public override bool IsValid => 
			base.IsValid && 
			Expression.NodeType == ExpressionType.MemberAccess;

		public string PropertyName =>
			Mapper.GetProperty(Expression.Member.Name).ColumnName;

		public override string Value
		{
			get
			{
				if (IsValid)
				{
					return PropertyName;
				}

				throw new NotSupportedException(
					$"The member '{PropertyName}' is not supported");
			}
		}

		public PropertyToken(MemberExpression expression, IEntityMapper mapper)
			:base(expression, mapper)
		{
		}
	}
}

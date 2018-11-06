using System;
using System.Linq.Expressions;
using Dapper.Linq.Tokens.Abstractions;

namespace Dapper.Linq.Tokens
{
	public class PropertyToken : ExpressionToken<MemberExpression>
	{
		public override bool IsValid => 
			base.IsValid && 
			Expression.NodeType == ExpressionType.MemberAccess;

		public string PropertyName =>
			Expression.Member.Name;

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

		public PropertyToken(MemberExpression expression)
			:base(expression)
		{
		}
	}
}

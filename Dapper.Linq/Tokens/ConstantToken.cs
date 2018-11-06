using System;
using System.Linq.Expressions;
using Dapper.Linq.Tokens.Abstractions;

namespace Dapper.Linq.Tokens
{
	public class ConstantToken : ExpressionToken<ConstantExpression>
	{
		public object ConstantValue =>
			Expression.Value;

		public TypeCode ConstantType =>
			Type.GetTypeCode(ConstantValue.GetType());

		public override string Value
		{
			get
			{
				if (ConstantValue == null)
				{
					return NullValue;
				}

				return this.ValueInternal();
			}
		}

		public ConstantToken(ConstantExpression expression) 
			: base(expression)
		{
		}

		private string ValueInternal()
		{
			switch (ConstantType)
			{
				case TypeCode.Boolean:
					return ((bool)ConstantValue) ? "1" : "0";
				case TypeCode.String:
					return $"'{ConstantValue}'";
				case TypeCode.Object:
					throw new NotSupportedException(
						$"The constant for '{ConstantValue}' is not supported");
				default:
					return ConstantValue.ToString();
			}
		}

		private const string NullValue = "NULL";
	}
}

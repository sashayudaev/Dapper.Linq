﻿using System;
using System.Linq.Expressions;
using Dapper.Linq.Core.Mappers;
using Dapper.Linq.Tokens.Abstractions;

namespace Dapper.Linq.Tokens
{
	public class OperationToken : ExpressionToken<BinaryExpression>
	{
		public ExpressionType Operation =>
			Expression.NodeType;

		public override string Value =>
			this.ResolveOperation();

		public OperationToken(BinaryExpression expression, IEntityMapper mapper) 
			: base(expression, mapper)
		{
		}

		private string ResolveOperation()
		{
			switch (Operation)
			{
				case ExpressionType.And:
				case ExpressionType.AndAlso:
					return " AND ";
				case ExpressionType.Or:
					return " OR";
				case ExpressionType.Equal:
					return " = ";
				case ExpressionType.NotEqual:
					return " <> ";
				case ExpressionType.LessThan:
					return " < ";
				case ExpressionType.LessThanOrEqual:
					return " <= ";
				case ExpressionType.GreaterThan:
					return " > ";
				case ExpressionType.GreaterThanOrEqual:
					return " >= ";
				default:
					throw new NotSupportedException(
						$"The binary operator '{Operation}' is not supported");
			}
		}
	}
}

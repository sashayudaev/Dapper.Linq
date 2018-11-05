using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Dapper.Linq.Core;
using Dapper.Linq.Helpers;
using Dapper.Linq.Predicates;

namespace Dapper.Linq.Queries
{
	public class QueryBuilder : ExpressionVisitor
	{
		public PredicateCollection Predicates { get; } =
			new PredicateCollection();

		public string Build(Expression expression)
		{
			this.Visit(expression);

			return Predicates.BuildQuery();
		}

		protected override Expression VisitMethodCall(MethodCallExpression expression)
		{
			if(expression.Method.DeclaringType != typeof(Queryable))
			{
				throw new NotSupportedException(
					$"The method '{expression.Method.Name}' is not supported");
			}

			var method = expression.Method.Name;
			if(EnumHelper.TryGetFromDescription(method, out PredicateType type))
			{
				var predicate = PredicateBase.Create(type, expression);
				Predicates.Add(predicate);
				this.VisitNext(expression);
			}

			return expression;
		}

		private void VisitNext(MethodCallExpression expression) =>
			this.Visit(expression.Arguments[0]);
	}
}

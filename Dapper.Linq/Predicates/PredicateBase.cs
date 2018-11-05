using System;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using Dapper.Linq.Core;

namespace Dapper.Linq.Predicates
{
	public abstract class PredicateBase : ExpressionVisitor, IPredicate
	{
		public StringBuilder Query { get; } =
			new StringBuilder();

		public abstract PredicateType PredicateType { get; }

		public MethodCallExpression Expression { get; }

		public PredicateBase(MethodCallExpression expression)
		{
			Expression = expression ?? 
				throw new ArgumentNullException(nameof(expression));
		}

		public static IPredicate Where(MethodCallExpression expression) =>
			new WherePredicate(expression);
		public static IPredicate OrderBy(MethodCallExpression expression) =>
			new OrderByPredicate(expression);

		public string BuildQuery(StringBuilder query)
		{
			this.Visit(Expression);
			return Query.ToString();
		}

		protected override Expression VisitUnary(UnaryExpression unary)
		{
			switch (unary.NodeType)
			{
				case ExpressionType.Not:
					Query.Append(" NOT ");
					this.Visit(unary.Operand);
					break;
				case ExpressionType.Quote:
					this.Visit(unary.Operand);
					break;
				default:
					throw new NotSupportedException(
						$"The unary operator '{unary.NodeType}' is not supported");
			}
			return unary;
		}

		protected override Expression VisitBinary(BinaryExpression binary)
		{
			Query.Append("(");
			this.Visit(binary.Left);
			this.VisitOperation(binary);
			this.Visit(binary.Right);
			Query.Append(")");
			return binary;
		}

		protected override Expression VisitConstant(ConstantExpression constant)
		{
			if (constant.Value is IQueryable queryable)
			{
				// assume constant nodes w/ IQueryables are table references
				Query.Append("SELECT * FROM ");
				Query.Append(queryable.ElementType.Name);
			}
			else if (constant.Value == null)
			{
				Query.Append("NULL");
			}
			else
			{
				switch (Type.GetTypeCode(constant.Value.GetType()))
				{
					case TypeCode.Boolean:
						Query.Append(((bool)constant.Value) ? 1 : 0);
						break;
					case TypeCode.String:
						Query.Append("'");
						Query.Append(constant.Value);
						Query.Append("'");
						break;
					case TypeCode.Object:
						throw new NotSupportedException(
						  string.Format(
						  "The constant for '{0}' is not supported", constant.Value));
					default:
						Query.Append(constant.Value);
						break;
				}
			}
			return constant;
		}

		protected override Expression VisitMember(MemberExpression member)
		{
			if (member.Expression?.NodeType == ExpressionType.Parameter)
			{
				Query.Append(member.Member.Name);
				return member;
			}

			throw new NotSupportedException(
				$"The member '{member.Member.Name}' is not supported");
		}

		protected static Expression StripQuotes(Expression e)
		{
			while (e.NodeType == ExpressionType.Quote)
			{
				e = ((UnaryExpression)e).Operand;
			}
			return e;
		}

		private void VisitOperation(BinaryExpression binary)
		{
			switch (binary.NodeType)
			{
				case ExpressionType.And:
					Query.Append(" AND ");
					break;
				case ExpressionType.Or:
					Query.Append(" OR");
					break;
				case ExpressionType.Equal:
					Query.Append(" = ");
					break;
				case ExpressionType.NotEqual:
					Query.Append(" <> ");
					break;
				case ExpressionType.LessThan:
					Query.Append(" < ");
					break;
				case ExpressionType.LessThanOrEqual:
					Query.Append(" <= ");
					break;
				case ExpressionType.GreaterThan:
					Query.Append(" > ");
					break;
				case ExpressionType.GreaterThanOrEqual:
					Query.Append(" >= ");
					break;
				default:
					throw new NotSupportedException(
					  string.Format(
						"The binary operator '{0}' is not supported", binary.NodeType));
			}
		}
	}
}

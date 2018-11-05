using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Dapper.Linq.Queries
{
	public class QueryBuilder : ExpressionVisitor
	{
		public StringBuilder Query { get; } =
			new StringBuilder();

		public string Build(Expression expression)
		{
			this.Visit(expression);
			return Query.ToString();
		}

		protected override Expression VisitMethodCall(MethodCallExpression m)
		{
			if (m.Method.DeclaringType == typeof(Queryable)
			  && m.Method.Name == "Where")
			{
				Query.Append("SELECT * FROM (");
				this.Visit(m.Arguments[0]);
				Query.Append(") AS T WHERE ");
				LambdaExpression lambda = (LambdaExpression)StripQuotes(m.Arguments[1]);
				this.Visit(lambda.Body);
				return m;
			}
			throw new NotSupportedException(string.Format(
			  "The method '{0}' is not supported", m.Method.Name));
		}

		protected override Expression VisitUnary(UnaryExpression u)
		{
			switch (u.NodeType)
			{
				case ExpressionType.Not:
					Query.Append(" NOT ");
					this.Visit(u.Operand);
					break;
				default:
					throw new NotSupportedException(string.Format(
					  "The unary operator '{0}' is not supported", u.NodeType));
			}
			return u;
		}

		protected override Expression VisitBinary(BinaryExpression b)
		{
			Query.Append("(");
			this.Visit(b.Left);
			switch (b.NodeType)
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
						"The binary operator '{0}' is not supported", b.NodeType));
			}
			this.Visit(b.Right);
			Query.Append(")");
			return b;
		}

		protected override Expression VisitConstant(ConstantExpression c)
		{
			IQueryable q = c.Value as IQueryable;
			if (q != null)
			{
				// assume constant nodes w/ IQueryables are table references
				Query.Append("SELECT * FROM ");
				Query.Append(q.ElementType.Name);
			}
			else if (c.Value == null)
			{
				Query.Append("NULL");
			}
			else
			{
				switch (Type.GetTypeCode(c.Value.GetType()))
				{
					case TypeCode.Boolean:
						Query.Append(((bool)c.Value) ? 1 : 0);
						break;
					case TypeCode.String:
						Query.Append("'");
						Query.Append(c.Value);
						Query.Append("'");
						break;
					case TypeCode.Object:
						throw new NotSupportedException(
						  string.Format(
						  "The constant for '{0}' is not supported", c.Value));
					default:
						Query.Append(c.Value);
						break;
				}
			}
			return c;
		}

		protected override Expression VisitMember(MemberExpression m)
		{
			if (m.Expression != null
			  && m.Expression.NodeType == ExpressionType.Parameter)
			{
				Query.Append(m.Member.Name);
				return m;
			}
			throw new NotSupportedException(
			  string.Format("The member '{0}' is not supported", m.Member.Name));
		}

		private static Expression StripQuotes(Expression e)
		{
			while (e.NodeType == ExpressionType.Quote)
			{
				e = ((UnaryExpression)e).Operand;
			}
			return e;
		}
	}
}

using System.Linq;
using System.Linq.Expressions;
using Dapper.Linq.Core;

namespace Dapper.Linq.Predicates
{
	public class SelectPredicate : PredicateBase
	{
		public override PredicateType PredicateType =>
			PredicateType.Select;

		public new ConstantExpression Expression =>
			(ConstantExpression)base.Expression;

		public SelectPredicate(Expression expression)
			: base(expression)
		{

		}

		protected override Expression VisitConstant(ConstantExpression constant)
		{
			if(constant.Value is IQueryable queryable)
			{
				var table = queryable.ElementType.Name;
				Query.Append($"SELECT * FROM public.{table}");
			}
			return constant;
		}
	}
}

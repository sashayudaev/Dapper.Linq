using System;
using System.Reflection;

namespace Dapper.Linq.Helpers
{
	public static class AttributeHelper
	{
		public static bool HasAttribute<TAttribute>(this object source, out TAttribute attribute)
			where TAttribute : Attribute => HasAttribute(source.GetType(), out attribute);

		public static bool HasAttribute<TAttribute>(this MemberInfo member, out TAttribute attribute)
			where TAttribute : Attribute
		{
			attribute = member.GetCustomAttribute<TAttribute>();
			return attribute != null;
		}
	}
}

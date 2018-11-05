using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Dapper.Linq.Helpers
{
	public static class EnumHelper
	{
		public static bool TryGetFromDescription<TEnum>(string description, out TEnum value)
			where TEnum : struct
		{
			value = default(TEnum);

			if(!typeof(TEnum).IsEnum)
			{
				return false;
			}

			var values = Enum
				.GetValues(typeof(TEnum))
				.Cast<TEnum>();

			try
			{
				value = values.First(source =>
					source.GetDescription().Equals(description));
				return true;
			}
			catch
			{
				return false;
			}

		}

		public static string GetDescription<TEnum>(this TEnum source)
			where TEnum : struct
		{
			var name = source.ToString();
			var attribute = source.GetType()
				.GetRuntimeField(name)
				.GetCustomAttribute<DescriptionAttribute>();

			return attribute != null
				? attribute.Description : name;
		}
	}
}

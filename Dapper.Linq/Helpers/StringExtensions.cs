using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Linq.Helpers
{
	public static class StringExtensions
	{
		public static bool OrdinalEquals(this string left, string right) =>
			String.Equals(left, right, StringComparison.Ordinal);

		public static StringBuilder RemoveLast(this StringBuilder source, int count = 1) =>
			source.Remove(source.Length - count, count);

		public static StringBuilder AppendRange(this StringBuilder source, IEnumerable<string> range)
		{
			foreach (var item in range)
			{
				source.Append(item);
			}
			return source;
		}
	}
}

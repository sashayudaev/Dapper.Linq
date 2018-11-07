using System;

namespace Dapper.Linq.Attributes
{
	public class ProcedureParameterAttribute : Attribute
	{
		public string Name { get; }

		public ProcedureParameterAttribute(string name)
		{
			Name = name ??
				throw new ArgumentNullException(nameof(name));
		}
	}
}

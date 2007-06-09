using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public enum PersistanceFieldType
	{
		/// <summary>
		/// This field does not map to any record
		/// </summary>
		None = 0,
		/// <summary>
		/// This field maps to this record
		/// </summary>
		Basic,
		/// <summary>
		/// This field maps to a child record
		/// </summary>
		OneToOneChild,
		/// <summary>
		/// This field maps to many children records
		/// </summary>
		OneToManyChildren
	}
}

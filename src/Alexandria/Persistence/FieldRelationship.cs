using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Persistence
{
	public enum FieldRelationship
	{
		/// <summary>
		/// This field does not map to any other records
		/// </summary>
		None = 0,
		/// <summary>
		/// This field maps to a child record
		/// </summary>
		OneToOne,
		/// <summary>
		/// This field represents a link back to its parent record
		/// </summary>
		OneToMany,
		/// <summary>
		/// This field and others all map to a child record
		/// </summary>
		ManyToOne,
		/// <summary>
		/// This field and others all map to many children records
		/// </summary>
		ManyToMany
	}
}

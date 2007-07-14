using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Persistence
{
	[AttributeUsage(AttributeTargets.Property)]
	public class IndexAttribute : Attribute
	{
		#region Constructors
		public IndexAttribute(string indexName)
		{
			this.indexName = indexName;
		}
		
		public IndexAttribute(string indexName, bool isUnique) : this(indexName)
		{
			this.isUnique = isUnique;
		}
		
		public IndexAttribute(string indexName, bool isUnique, int ordinal) : this(indexName, isUnique)
		{
			this.ordinal = ordinal;
		}
		#endregion
		
		#region Private Fields
		private string indexName;
		private bool isUnique;		
		private int ordinal;
		#endregion
		
		#region Public Properties
		public string IndexName
		{
			get { return indexName; }
		}
		
		public bool IsUnique
		{
			get { return isUnique; }
		}
		
		public int Ordinal
		{
			get { return ordinal; }
		}
		#endregion
	}
}

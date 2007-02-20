using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria.Data
{
	public abstract class DataRecord
	{
		#region Private Fields
		private string id;
		#endregion
		
		#region Constructors
		protected DataRecord()
		{
		}
		
		protected DataRecord(string id)
		{
			this.id = id;
		}
		#endregion
		
		#region Public Properties
		public string Id
		{
			get {return id;}
			protected set {id = value;}
		}
		#endregion
	}
}

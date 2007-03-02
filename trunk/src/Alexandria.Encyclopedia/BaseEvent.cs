using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria
{
	public abstract class BaseEvent
	{
		#region Private Fields
		private string type;
		private DateTime date;
		#endregion
		
		#region Constructors
		protected BaseEvent(string type)
		{
			this.type = type;
		}
		
		protected BaseEvent(string id, string type)
		{
			this.type = type;
		}
		#endregion
		
		#region Public Properties
		public string Type
		{
			get {return type;}	
		}
		
		public DateTime Date
		{
			get {return date;}
			set {date = value;}
		}
		#endregion
	}
}

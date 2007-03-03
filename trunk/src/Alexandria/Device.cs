using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public abstract class Device
	{
		#region Private Fields
		private string name;
		#endregion
		
		#region Constructors
		protected Device()
		{
		}
		
		protected Device(string name)
		{
			this.name = name;
		}
		#endregion
		
		#region Public Properties
		public string Name
		{
			get {return name;}
			protected set {name = value;}
		}
		#endregion
	}
}

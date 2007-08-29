using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public class ModelChangingEventArgs : EventArgs
	{
		public ModelChangingEventArgs(string propertyName, object newValue)
		{
			this.propertyName = propertyName;
			this.newValue = newValue;
		}
		
		private string propertyName;
		private object newValue;
		private bool isValid;
		
		public string PropertyName
		{
			get { return propertyName; }
		}
		
		public object NewValue
		{
			get { return newValue; }
		}
		
		public bool IsValid
		{
			get { return isValid; }
			set { isValid = value; }
		}
	}
}
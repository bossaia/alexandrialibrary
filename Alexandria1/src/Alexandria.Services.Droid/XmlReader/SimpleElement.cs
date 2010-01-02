using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;
using Alexandria.Droid;

namespace Alexandria.Droid.XmlReader
{
	public class SimpleElement
	{
		#region Private Fields
		string myText = string.Empty;
		#endregion
		
		#region Public Methods
		/* setters */
		public void SetText(string theText)
		{
			this.myText += theText;
		}
		
		public virtual void SetAttributeValue(string name, string value)
		{
			MessageDisplay.UnknownAttributeWarning(name, this.GetElementName());
		}

		/* getters */
		public string GetText(){ return myText; }
		
		public string GetElementName()
		{
			string className = this.GetType().Name; //this.getClass().getName();
			className = className.Substring(className.LastIndexOf(".") + 1);
			return className;
		}

		/** method to be overridden in cases where the element content needs to be specified
		 * only when the end of element tag is reached
		 */
		public void CompleteElementContent()
		{
		}
		#endregion
	}
}

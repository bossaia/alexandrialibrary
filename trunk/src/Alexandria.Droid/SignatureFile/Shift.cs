using System;
using System.Collections.Generic;
using System.Text;
using AlexandriaOrg.Alexandria.Droid.XmlReader;

namespace AlexandriaOrg.Alexandria.Droid.SignatureFile
{
	public class Shift : SimpleElement
	{
		#region Private Fields
		long myShiftValue;
		int myShiftByte = 999;
		#endregion

		#region Public Methods
		/** Set the shift distance when the end of element tag is reached.  
		 * This will have been stored in the text attribute by the setText method defined in SimpleElement
		 */
		public void completeElementContent()
		{
			String theElementValue = this.GetText();   //((SimpleElement)this).getText();
			try
			{
				this.myShiftValue = Convert.ToInt64(theElementValue); //Long.parseLong(theElementValue);
			}
			catch (Exception)
			{
				MessageDisplay.GeneralWarning("The following non-numerical shift distance was found in the signature file: " + theElementValue);
				this.myShiftValue = 1;
			}

		}

		/** Respond to an XML attribute
		 * @param theName attribute name
		 * @param theValue attribute value
		 */
		public void setAttributeValue(String theName, String theValue)
		{
			if (theName.Equals("Byte"))
			{
				try
				{
					myShiftByte = Convert.ToInt32(theValue); //Integer.parseInt(theValue, 16);
				}
				catch (Exception)
				{
				}
			}
			else
			{
				MessageDisplay.UnknownAttributeWarning(theName, this.GetElementName());
			}
		}

		/* getters */
		public int getShiftByte() { return myShiftByte; }
		public long getShiftValue() { return myShiftValue; }
		#endregion
	}
}
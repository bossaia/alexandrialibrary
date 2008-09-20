using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;
using Alexandria.Droid.XmlReader;

namespace Alexandria.Droid.SignatureFile
{
	public class SideFragment : SimpleElement
	{
		#region Private Fields
		int myPosition;
		int myMinOffset;
		int myMaxOffset;
		int numBytes;
		string mySequenceFragment;
		List<ByteSequenceSpecifier> myByteSpecifierSequence;
		#endregion

		#region Public Methods
		/* setters */
		public void setPosition(int thePosition) { this.myPosition = thePosition; }
		public void setMinOffset(int theMinOffset) { this.myMinOffset = theMinOffset; }
		public void setMaxOffset(int theMaxOffset) { this.myMaxOffset = theMaxOffset; }
		public void setAttributeValue(string name, string value)
		{
			if (name.Equals("Position"))
			{
				setPosition(Convert.ToInt32(value)); //Integer.parseInt(value));
			}
			else if (name.Equals("MinOffset"))
			{
				setMinOffset(Convert.ToInt32(value)); //(Integer.parseInt(value));
			}
			else if (name.Equals("MaxOffset"))
			{
				setMaxOffset(Convert.ToInt32(value)); //(Integer.parseInt(value));
			}
			else
			{
				MessageDisplay.UnknownAttributeWarning(name, this.GetElementName());
			}
		}

		/* getters */
		public int getPosition() { return myPosition; }
		public int getMinOffset() { return myMinOffset; }
		public int getMaxOffset() { return myMaxOffset; }
		public int getNumByteSeqSpecifiers() { return myByteSpecifierSequence.Count; }    // Number of byte sequence specifiers we hold (each of which specifies at least one byte)
		public ByteSequenceSpecifier getByteSeqSpecifier(int index) { return myByteSpecifierSequence[index]; }
		public int getNumBytes() { return numBytes; }                                      // Total number of bytes we hold
		public String getSequence() { return mySequenceFragment; }

		/** Set the sideFragment sequence (this will have been stored in the text attribute by the setText method).
		 * Then transforms the input string into an array of bytes
		 */
		public void completeElementContent()
		{
			numBytes = 0;
			string theElementValue = this.GetText(); //.getText();
			this.mySequenceFragment = theElementValue;
			myByteSpecifierSequence = new List<ByteSequenceSpecifier>(); //ArrayList();
			//StringBuffer allSpecifiers = new StringBuffer(theElementValue);
			StringBuilder allSpecifiers = new StringBuilder(theElementValue);
			while (allSpecifiers.Length > 0)
			{
				try
				{
					ByteSequenceSpecifier bss = new ByteSequenceSpecifier(allSpecifiers);
					myByteSpecifierSequence.Add(bss);
					numBytes += bss.getNumBytes();
				}
				catch (Exception)
				{
				}
			}
		}
		#endregion
	}
}

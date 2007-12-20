using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;

namespace Alexandria.Droid.SignatureFile
{
	public class FileFormat : XmlReader.SimpleElement
	{
		#region Private Fields
		private int identifier;
		private string name;
		private string version;
		private string puid;
		private IList<string> internalSigIDs = new List<string>();
		private IList<string> extensions = new List<string>();
		private IList<string> hasPriorityOver = new List<string>();
		#endregion
		
		#region Private Constant Fields
		private const string ID_ATTRIBUTE = "ID";
		private const string NAME_ATTRIBUTE = "Name";
		private const string VERSION_ATTRIBUTE = "Version";
		private const string PUID_ATTRIBUTE = "PUID";
		#endregion

		#region Public Methods
		
		#region Setters
		/* setters */
		public void SetInternalSignatureID(string theID) { this.internalSigIDs.Add(theID); }
		public void SetExtension(string theExtension) { this.extensions.Add(theExtension); }
		public void SetHasPriorityOverFileFormatID(String theID) { this.hasPriorityOver.Add(theID); }
		public void setAttributeValue(string theName, string theValue)
		{
			if (theName.Equals(ID_ATTRIBUTE))
			{
				this.identifier = Convert.ToInt32(theValue); //Integer.parseInt(theValue);
			}
			else if (theName.Equals(NAME_ATTRIBUTE))
			{
				this.name = theValue;
			}
			else if (theName.Equals(VERSION_ATTRIBUTE))
			{
				this.version = theValue;
			}
			else if (theName.Equals(PUID_ATTRIBUTE))
			{
				this.puid = theValue;
			}
			else
			{
				MessageDisplay.UnknownAttributeWarning(name, this.GetElementName());
			}
		}
		#endregion

		#region Getters
		/* getters */
		public int GetNumberOfInternalSignatures() { return this.internalSigIDs.Count; }
		public int GetNumberOfExtensions() { return this.extensions.Count; }
		public int GetNumberOfHasPriorityOver() { return this.hasPriorityOver.Count; }
		public int GetInternalSignatureId(int theIndex) { return Convert.ToInt32(this.internalSigIDs[theIndex].ToString()); } //Integer.parseInt(this.internalSigIDs.get(theIndex).toString()); }
		public string GetExtension(int theIndex) { return this.extensions[theIndex].ToString(); } //.get(theIndex).toString(); }
		public int GetHasPriorityOver(int theIndex) { return Convert.ToInt32(this.hasPriorityOver[theIndex].ToString()); } //Integer.parseInt(this.hasPriorityOver.get(theIndex).toString()); }
		public int GetId() { return identifier; }
		public string GetName() { return name; }
		public string GetVersion() { return version; }
		public string GetPuid() { return puid; }
		#endregion

		/**
		 * Indicates whether the file extension given is valid for this file format
		 *
		 * @param theExtension   file extension
		 */
		public bool HasMatchingExtension(string theExtension)
		{
			bool matchingExtension = false;
			for (int iExtension = 0; iExtension < this.GetNumberOfExtensions(); iExtension++)
			{
				//theExtension.equalsIgnoreCase(this.getExtension(iExtension)))
				if (string.Compare(theExtension, this.GetExtension(iExtension), true) == 0)				
				{
					matchingExtension = true;
				}
			}//loop through Extensions
			return matchingExtension;
		}
		#endregion
	}
}

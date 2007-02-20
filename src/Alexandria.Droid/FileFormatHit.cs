using System;
using System.Collections.Generic;
using System.Text;
using AlexandriaOrg.Alexandria.Droid.SignatureFile;

namespace AlexandriaOrg.Alexandria.Droid
{
	public class FileFormatHit : XmlReader.SimpleElement
	{
		#region Constructors
		/**
		 * Creates a new blank instance of fileFormatHit
		 *
		 * @param theFileFormat     The file format which has been identified
		 * @param theType           The type of hit i.e. Positive/tentative
		 * @param theSpecificity    Flag is set to true for Positive specific hits
		 * @param theWarning        A warning associated with the hit
		 */
		public FileFormatHit(SignatureFile.FileFormat theFileFormat, int theType, bool theSpecificity, string theWarning)
		{
			myHitFileFormat = theFileFormat;
			if (theType == AnalysisController.HIT_TYPE_POSITIVE_GENERIC_OR_SPECIFIC)
			{
				if (theSpecificity)
				{
					myHitType = AnalysisController.HIT_TYPE_POSITIVE_SPECIFIC;
				}
				else
				{
					myHitType = AnalysisController.HIT_TYPE_POSITIVE_GENERIC;
				}
			}
			else
			{
				myHitType = theType;
			}
			this.SetIdentificationWarning(theWarning);
		}

		public FileFormatHit()
		{
		}
		#endregion
	
		#region Private Fields
		private string myHitWarning = string.Empty;
		private int myHitType;
		private SignatureFile.FileFormat myHitFileFormat;
		#endregion
		
		#region Public Methods
		/**
		* Updates the warning message for a hit
		*
		*  Used by XML reader for IdentificationFile/FileFormatHit/IdentificationWarning element
		*
		* @param theWarning    A warning associated with the hit
		*/
		public void SetIdentificationWarning(string theWarning)
		{
			myHitWarning = theWarning;
		}

		/**
		* get the fileFormat for the hit
		*/
		public SignatureFile.FileFormat GetFileFormat()
		{
			return myHitFileFormat;
		}

		/**
		* get the name of the fileFormat of this hit
		*/
		public string GetFileFormatName()
		{
			return myHitFileFormat.GetName();
		}

		/**
		* get the version of the fileFormat of this hit
		*/
		public string GetFileFormatVersion()
		{
			return myHitFileFormat.GetVersion();
		}

		/**
		* get the PUID of the fileFormat of this hit
		*/
		public string GetFileFormatPUID()
		{
			return myHitFileFormat.GetPuid();
		}

		/**
		* get the code of the hit type
		*/
		public int GetHitType()
		{
			return myHitType;
		}

		/**
		* get the name of the hit type
		*/
		public string GetHitTypeVerbose()
		{
			string theHitType = string.Empty;
			if (myHitType == AnalysisController.HIT_TYPE_POSITIVE_GENERIC)
			{
				theHitType = AnalysisController.HIT_TYPE_POSITIVE_GENERIC_TEXT;
			}
			else if (myHitType == AnalysisController.HIT_TYPE_POSITIVE_SPECIFIC)
			{
				theHitType = AnalysisController.HIT_TYPE_POSITIVE_SPECIFIC_TEXT;
			}
			else if (myHitType == AnalysisController.HIT_TYPE_TENTATIVE)
			{
				theHitType = AnalysisController.HIT_TYPE_TENTATIVE_TEXT;
			}
			else if (myHitType == AnalysisController.HIT_TYPE_POSITIVE_GENERIC_OR_SPECIFIC)
			{
				theHitType = AnalysisController.HIT_TYPE_POSITIVE_GENERIC_OR_SPECIFIC_TEXT;
			}
			return theHitType;
		}

		/**
		* get any warning associated with the hit
		*/
		public string GetHitWarning()
		{
			return myHitWarning;
		}

		/**
		* For positive hits, this returns true if hit is Specific
		* or returns false if hit is Generic.
		* Meaningless for Tentative hits. (though returns false)
		*/
		public bool IsSpecific()
		{
			if (myHitType == AnalysisController.HIT_TYPE_POSITIVE_SPECIFIC)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/**
		*  Populates the details of the IdentificationFile when it is read in from XML file
		*  @param theName Name of the attribute read in
		*  @param theValue Value of the attribute read in
		*/
		public override void SetAttributeValue(string theName, string theValue)
		{
			if (theName.Equals("HitStatus"))
			{
				this.SetStatus(theValue);
			}
			else if (theName.Equals("FormatName"))
			{
				this.SetName(theValue);
			}
			else if (theName.Equals("FormatVersion"))
			{
				this.SetVersion(theValue);
			}
			else if (theName.Equals("FormatPUID"))
			{
				this.SetPUID(theValue);
			}
			else if (theName.Equals("HitWarning"))
			{
				this.SetIdentificationWarning(theValue);
			}
			else
			{
				MessageDisplay.UnknownAttributeWarning(theName, this.GetElementName());
			}
		}

		/** 
		* Set hit status.  Used by XML reader for IdentificationFile/FileFormatHit/Status element
		*/
		public void SetStatus(String value)
		{
			//String value = element.getText();
			if (value.Equals(AnalysisController.HIT_TYPE_POSITIVE_GENERIC_TEXT))
			{
				myHitType = AnalysisController.HIT_TYPE_POSITIVE_GENERIC;
			}
			else if (value.Equals(AnalysisController.HIT_TYPE_POSITIVE_SPECIFIC_TEXT))
			{
				myHitType = AnalysisController.HIT_TYPE_POSITIVE_SPECIFIC;
			}
			else if (value.Equals(AnalysisController.HIT_TYPE_TENTATIVE_TEXT))
			{
				myHitType = AnalysisController.HIT_TYPE_TENTATIVE;
			}
			else if (value.Equals(AnalysisController.HIT_TYPE_POSITIVE_GENERIC_OR_SPECIFIC_TEXT))
			{
				myHitType = AnalysisController.HIT_TYPE_POSITIVE_GENERIC_OR_SPECIFIC;
			}
			else
			{
				MessageDisplay.GeneralWarning("Unknown hit status listed: " + value);
			}
		}

		/** 
		* Set hit format name.  Used by XML reader for IdentificationFile/FileFormatHit/Name element
		*/
		public void SetName(String value)
		{
			//if necessary, this creates a new dummy File format
			if (myHitFileFormat == null)
			{
				myHitFileFormat = new FileFormat();
			}
			myHitFileFormat.setAttributeValue("Name", value);
		}

		/** 
		* Set hit format version.  Used by XML reader for IdentificationFile/FileFormatHit/Version element
		*/
		public void SetVersion(String value)
		{
			if (myHitFileFormat == null)
			{
				myHitFileFormat = new FileFormat();
			}
			myHitFileFormat.setAttributeValue("Version", value);
		}

		/** 
		* Set hit format PUID.  Used by XML reader for IdentificationFile/FileFormatHit/PUID element
		*/
		public void SetPUID(String value)
		{
			if (myHitFileFormat == null)
			{
				myHitFileFormat = new FileFormat();
			}
			myHitFileFormat.setAttributeValue("PUID", value);
		}
		#endregion
	}
}

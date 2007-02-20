using System;
using System.Collections.Generic;
using System.Text;
using AlexandriaOrg.Alexandria.Droid.BinaryReader;
using AlexandriaOrg.Alexandria.Droid.XmlReader;

namespace AlexandriaOrg.Alexandria.Droid
{
	public class IdentificationFile : SimpleElement
	{
	    private string identificationWarning = string.Empty;
		private int myIDStatus = AnalysisController.FILE_CLASSIFICATION_NOTCLASSIFIED ;
		private System.Collections.IList fileHits  = new System.Collections.ArrayList();
		private string filePath;
    
		/** Creates a new instance of IdentificationFile.
		*  @param  filePath    Full file path
		*/
		public IdentificationFile(String filePath)
		{
			setFilePath(filePath);
		}
		
		public IdentificationFile()
		{
		}
    
		/**
		*  Set the file identification status.
		*  @param theStatus file identification status
		*/
		public void setIDStatus(int theStatus)
		{
			myIDStatus = theStatus;
		}
    
		/**Sets the file status to Postive*/
		public void setPositiveIdent()
		{
			myIDStatus = AnalysisController.FILE_CLASSIFICATION_POSITIVE;
		}
		
		/**Sets the file status to tentitive */
		public void setTentativeIdent()
		{
			myIDStatus = AnalysisController.FILE_CLASSIFICATION_TENTATIVE;
		}
		
		/**Sets the status to not identified*/
		public void setNoIdent()
		{
			myIDStatus = AnalysisController.FILE_CLASSIFICATION_NOHIT;
		}
		
		/**Sets the status to error during identification*/
		public void setErrorIdent()
		{
			myIDStatus = AnalysisController.FILE_CLASSIFICATION_ERROR;
		}
    
		/**
		*  Set the file identification warning.
		*  @param  warning file identification warning
		*/
		public void setWarning(String warning)
		{
			identificationWarning = warning;
		}
    
		/**
		*  Set the full file Path.
		*  @param filePath full file path
		*/
		public void setFilePath(string filePath)
		{
			this.filePath = filePath;
		}
    
		/**
		*  Add a file format hit for this file.
		*  @param  hit file format hit found
		*/
		public void addHit(FileFormatHit hit)
		{
			fileHits.Add(hit) ;
		}
    
		/**
		* Add a file format hit for this file.
		*
		* <p>Used for reading XML 
		*  @param theHit file format hit found
		*/
		public void addFileFormatHit(FileFormatHit theHit)
		{
			fileHits.Add(theHit);
		}
    
		/**
		*  Remove a file format hit for the file.
		*  @param index position in hit list of file
		*/
		public void removeHit(int index)
		{
			fileHits.Remove(index);
		}
    
    
		/**
		*  Returns the file name (without the full path)
		*/
		public string getFileName()
		{
			if (InputStreamByteReader.isInputStream(filePath))
			{
				return string.Empty; // Input stream has no file name
			}
			else if (UrlByteReader.isURL(filePath))
			{
				// File part of URL
				return UrlByteReader.getURL(filePath).AbsolutePath; //.getFile();
			}
			else
			{
				return new System.IO.FileInfo(filePath).Name;
				//java.io.File(filePath)).getName();
			}
		}
    
		/**
		*  Returns the full file path
		*/
		public String getFilePath()
		{
			return filePath ;
		}
    
		/**
		*  Checks whether the file has been classified yet
		*  (i.e return YES for the following classification values:
		*  FILE_CLASSIFICATION_POSITIVE , FILE_CLASSIFICATION_TENTITIVE
		*  FILE_CLASSIFICATION_NOHIT , FILE_CLASSIFICATION_ERROR
		*/
		public bool isClassified()
		{
			return (myIDStatus != AnalysisController.FILE_CLASSIFICATION_NOTCLASSIFIED);
		}
    
		/**
		*  Returns the file classification found by the identification
		*  The options are setup as constants under Analysis Controller:
		*      *FILE_CLASSIFICATION_POSITIVE
		*      *FILE_CLASSIFICATION_TENTATIVE
		*      *FILE_CLASSIFICATION_NOHIT
		*      *FILE_CLASSIFICATION_ERROR
		*      *FILE_CLASSIFICATION_NOTCLASSIFIED
		*/
		public int getClassification()
		{
			return myIDStatus;
		}
    
		/**
		*  Returns the text description of the file classification found by the identification
		*  The options are setup as constants under Analysis Controller:
		*      *FILE_CLASSIFICATION_POSITIVE_TEXT
		*      *FILE_CLASSIFICATION_TENTATIVE_TEXT
		*      *FILE_CLASSIFICATION_NOHIT_TEXT
		*      *FILE_CLASSIFICATION_ERROR_TEXT
		*      *FILE_CLASSIFICATION_NOTCLASSIFIED_TEXT
		*/
		public String getClassificationText()
		{
			string theClassificationText = string.Empty;
			if (myIDStatus == AnalysisController.FILE_CLASSIFICATION_POSITIVE)
			{
				theClassificationText = AnalysisController.FILE_CLASSIFICATION_POSITIVE_TEXT;
			}
			else if (myIDStatus == AnalysisController.FILE_CLASSIFICATION_TENTATIVE)
			{
				theClassificationText = AnalysisController.FILE_CLASSIFICATION_TENTATIVE_TEXT;
			}
			else if (myIDStatus == AnalysisController.FILE_CLASSIFICATION_NOHIT)
			{
				theClassificationText = AnalysisController.FILE_CLASSIFICATION_NOHIT_TEXT;
			}
			else if (myIDStatus == AnalysisController.FILE_CLASSIFICATION_ERROR)
			{
				theClassificationText = AnalysisController.FILE_CLASSIFICATION_ERROR_TEXT;
			}
			else if (myIDStatus == AnalysisController.FILE_CLASSIFICATION_NOTCLASSIFIED)
			{
				theClassificationText = AnalysisController.FILE_CLASSIFICATION_NOTCLASSIFIED_TEXT;
			}
			return theClassificationText;
		}
    
		/**
		*  Returns any warning associated with the file
		*/
		public String getWarning()
		{
			return identificationWarning;
		}
    
		/**
		* Returns number of hits found for this file
		*/
		public int getNumHits()
		{
			return fileHits.Count;
		}
    
		/**
		*  Returns a hit object associated with the file that has been run
		*  through the identification process
		*/
		public FileFormatHit getHit(int theIndex)
		{
			return (FileFormatHit)fileHits[theIndex];
		}
		
		/**
		*  Populate the details of the IdentificationFile object when read in from XML file.
		*  @param theName Name of the attribute read in
		*  @param theValue Value of the attribute read in
		*/
		public void setAttributeValue(string theName, string theValue)
		{
			if (theName.Equals("Name"))
			{
				this.setFilePath(theValue);
			}
			else if (theName.Equals("IdentQuality"))
			{
				//The IdentQuality attribute value should match one of those specified in code -
				//otherwise show a warning
				if (theValue.Equals(AnalysisController.FILE_CLASSIFICATION_POSITIVE_TEXT))
				{
					myIDStatus = AnalysisController.FILE_CLASSIFICATION_POSITIVE;
				}
				else if (theValue.Equals(AnalysisController.FILE_CLASSIFICATION_TENTATIVE_TEXT))
				{
					myIDStatus = AnalysisController.FILE_CLASSIFICATION_TENTATIVE;
				}
				else if (theValue.Equals(AnalysisController.FILE_CLASSIFICATION_NOHIT_TEXT))
				{
					myIDStatus = AnalysisController.FILE_CLASSIFICATION_NOHIT;
				}
				else if(theValue.Equals(AnalysisController.FILE_CLASSIFICATION_ERROR_TEXT))
				{
					myIDStatus = AnalysisController.FILE_CLASSIFICATION_ERROR;
				}
				else if(theValue.Equals(AnalysisController.FILE_CLASSIFICATION_NOTCLASSIFIED_TEXT))
				{
					myIDStatus = AnalysisController.FILE_CLASSIFICATION_NOTCLASSIFIED;
				}
				else
				{
					MessageDisplay.GeneralWarning("Unknown file status listed: <"+theValue+"> is not the same as <"+AnalysisController.FILE_CLASSIFICATION_POSITIVE_TEXT+">");
				}
			}
			else if (theName.Equals("Warning"))
			{
				this.setWarning(theValue);
			}
			else
			{
				MessageDisplay.UnknownAttributeWarning(theName, this.GetElementName());
			}
		}
	}
}

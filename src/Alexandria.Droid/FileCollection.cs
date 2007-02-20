using System;
using System.Collections.Generic;
using System.Text;
using AlexandriaOrg.Alexandria.Droid.BinaryReader;
using AlexandriaOrg.Alexandria.Droid.XmlReader;

namespace AlexandriaOrg.Alexandria.Droid
{
	public class FileCollection : SimpleElement
	{
		/** file path to store file list */
		private String myFileName;
		/** Holds IdentificationFile objects */
		private List<IdentificationFile> myFiles;

		/** Signature file version for a file that is read in -
		 * only used to check it is the same as currently loaded */
		private int mySigFileVersion = 0;

		/**
		 *Creates a FileCollection object
		 */
		public FileCollection()
		{
			myFileName = AnalysisController.FILE_LIST_FILE_NAME;
			myFiles = new List<IdentificationFile>(); //ArrayList();
		}

		/**
		 *  Specify the file path for where to store the file list
		 *  @param  theFileName     path of where to save the file
		 */
		public void setFileName(String theFileName)
		{
			myFileName = theFileName;
		}

		/**
		 *  Adds an element/elements to the collection
		 *  If filepath is a path to file then add that file
		 *  If filepath is a folder path then add contents of the folder
		 *
		 *  @param  theFile     Filepath of file or folder
		 *  @param  isRecursive if true add all subfolders and subsubfolders , etc
		 */
		public void addFile(string theFile, bool isRecursive)
		{

			if (UrlByteReader.isURL(theFile))
			{
				if (!this.isDuplicate(theFile))
				{
					//File object is a URL: add if it isn't a duplicate
					myFiles.Add(new IdentificationFile(theFile));
				}
				return;
			}

			if (InputStreamByteReader.isInputStream(theFile))
			{
				if (!this.isDuplicate(theFile))
				{
					// File is a the input stram: add if it isn't a duplicate
					myFiles.Add(new IdentificationFile(theFile));
				}
			}

			try
			{
				//java.io.File f = new java.io.File(theFile);
				System.IO.FileInfo f = new System.IO.FileInfo(theFile);


				//Is file object a directory or file?
				if (System.IO.Directory.Exists(theFile)) //f.isDirectory())
				{

					//File object is a directory/folder
					//Iterate through directory ,create IdentificationFile objects
					//and add them to the collection
					
					
					//java.io.File[] folderFiles = f.listFiles();
					System.IO.DirectoryInfo d = new System.IO.DirectoryInfo(theFile);
					System.IO.FileInfo[] folderFiles = d.GetFiles();
					
					int numFiles = 0;
					try
					{
						numFiles = folderFiles.Length;
					}
					catch (Exception)
					{
						MessageDisplay.GeneralWarning("Unable to read directory " + theFile + "\nThis may be because you do not have the correct permissions.");
					}
					for (int m = 0; m < numFiles; m++)
					{
						if (System.IO.Directory.Exists(folderFiles[m].FullName)) //folderFiles[m].isFile())
						{
							//If file exists and not duplicate then add
							if (!this.isDuplicate(folderFiles[m].FullName))
							{
								IdentificationFile idFile = new IdentificationFile(folderFiles[m].FullName);
								myFiles.Add(idFile);
							}

						}
						else if (System.IO.Directory.Exists(folderFiles[m].FullName) && isRecursive)
						{
							//If subdirectory found and recursive is on add contents of that folder
							addFile(folderFiles[m].FullName, isRecursive);
						}
					}

				}
				else if (!System.IO.Directory.Exists(f.FullName)) //f.isFile())
				{
					if (!this.isDuplicate(f.FullName))
					{
						//File object is a File then add file if it isn't a duplicate
						IdentificationFile idFile = new IdentificationFile(f.FullName);
						myFiles.Add(idFile);
					}
				}

			}
			catch (Exception e)
			{
				MessageDisplay.GeneralWarning("The following error occured while adding " + theFile + ":\n" + e.ToString());
			}

		}


		/**
		 *Checks whether the file already exists in file collection
		 *@param theFileName Full file name of file
		 */
		private bool isDuplicate(String theFileName)
		{
			for (int i = 0; i < myFiles.Count; i++)
			{
				if (string.Compare(this.getFile(i).getFilePath(), theFileName, true) == 0) //this.getFile(i).getFilePath().equalsIgnoreCase(theFileName))
				{
					return true;
				}
			}
			return false;
		}


		/**
		 *  Add a single file or folder to the collection
		 *  @param  theFile path to file or folder
		 */
		public void setFile(string theFile)
		{
			this.addFile(theFile, false);
		}


		/**
		 *Remove file from the file list
		 *@param theFileName Full file name of file to remove
		 */
		public void removeFile(string theFileName)
		{
			for (int i = 0; i < this.getNumFiles(); i++)
			{
				if (this.getFile(i).getFilePath().Equals(theFileName))
				{
					this.removeFile(i);
				}
			}
		}

		/**
		 *Remove file from the file list
		 *@param theFileIndex Index of file to remove
		 */
		public void removeFile(int theFileIndex)
		{
			if (theFileIndex < myFiles.Count)
			{
				myFiles.RemoveAt(theFileIndex); // .remove(theFileIndex);
			}
		}

		/**
		 * Empty file list
		 */
		public void removeAll()
		{
			myFiles.Clear();
		}

		/**
		 *  Gets the file name where file list stored
		 *  @return file name where file list stored
		 */
		public String getFileName() { return myFileName; }
		/**
		 *  Gets the IdentificationFile object by position in collection
		 *  @param theIndex position of element in collection
		 *  @return Specified IdentificationFile object
		 */
		public IdentificationFile getFile(int theIndex) { return (IdentificationFile)myFiles[theIndex]; }

		/**
		 *Remove file from the file list
		 *@param theFileName Full file name of file to remove
		 */
		public IdentificationFile getFile(String theFileName)
		{
			for (int i = 0; i < this.getNumFiles(); i++)
			{
				if (this.getFile(i).getFilePath().Equals(theFileName))
				{
					return this.getFile(i);
				}
			}

			return null;
		}

		/**
		 *  Get the number of files in the collection
		 *  @return no. of files in the collection
		 */
		public int getNumFiles() { return myFiles.Count; }

		/**
		 *  Add a new identification file to list (used when reading in an XML file collection file)
		 *  @param theFile A new IdentificationFile object which will be populated from file
		 */
		public void addIdentificationFile(IdentificationFile theFile)
		{
			myFiles.Add(theFile);
		}


		/**
		 *  Populates the details of the FileCollection when read in from XML file
		 *  @param theName Name of the attribute read in
		 *  @param theValue Value of the attribute read in
		 */
		public void setAttributeValue(String theName, String theValue)
		{
			if (theName.Equals(AnalysisController.LABEL_APPLICATION_VERSION))
			{
				setDROIDVersion(theValue);
			}
			else if (theName.Equals("SigFileVersion"))
			{
				setSignatureFileVersion(theValue);
			}
			else if (theName.Equals(AnalysisController.LABEL_DATE_CREATED))
			{
				setDateCreated(theValue);
			}
			else
			{
				MessageDisplay.UnknownAttributeWarning(theName, this.GetElementName());
			}
		}

		public void setDROIDVersion(String value)
		{
			if (!value.Equals(AnalysisController.getFFITVersion()))
			{
				MessageDisplay.GeneralWarning("This file was generated with DROID version " + value + ".  Current version is " + AnalysisController.getFFITVersion());
			}
		}

		public void setSignatureFileVersion(String value)
		{
			try
			{
				mySigFileVersion = Convert.ToInt32(value); // Integer.parseInt(value);
			}
			catch (InvalidCastException) //NumberFormatException)
			{
				MessageDisplay.GeneralWarning("The SigFileVersion attribute should be an integer");
			}
		}

		public void setDateCreated(String value)
		{
			// Ignore the contents of this element
		}

		/**
		 * returns the signature file version recorded in the file collection file
		 */
		public int getLoadedFileSigFileVersion()
		{
			return mySigFileVersion;
		}
	}
}

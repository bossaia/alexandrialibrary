using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;

namespace Alexandria.Droid.BinaryReader
{
	public class AbstractByteReader : IByteReader
	{
		/** The file represented by this object */
		protected IdentificationFile myIDFile;

		/**
		 * Creates a ByteReader object, and depending on the readFile setting,
		 * it may or may not read in the binary file specified
		 *
		 * @param theIDFile   The file to be read in
		 */
		protected AbstractByteReader(IdentificationFile theIDFile)
		{
			myIDFile = theIDFile;
		}

		/**
		 * Creates a ByteReader object, and depending on the readFile setting,
		 * it may or may not read in the binary file specified
		 *
		 * @param theIDFile   The file to be read in
		 */
		public static IByteReader newByteReader(IdentificationFile theIDFile) // was ByteReader
		{
			return newByteReader(theIDFile, true);
		}

		/**
		 * Static constructor for a ByteReader object, and depending on the readFile setting,
		 * it may or may not read in the binary file specified.
		 * 
		 * This may create a FileByteReader, UrlByteReader or InputStreamByteReader, depending on the
		 * the nature of the IdentificationFile passed in.
		 * 
		 * 
		 * @param theIDFile   The file to be read in
		 * @param readFile   Flag specifying whether file should be read in or not
		 */
		public static IByteReader newByteReader(IdentificationFile theIDFile, bool readFile) //was ByteReader
		{
			if (InputStreamByteReader.isInputStream(theIDFile.getFilePath()))
			{
				return InputStreamByteReader.newInputStreamByteReader(theIDFile, readFile);
			}
			else if (UrlByteReader.isURL(theIDFile.getFilePath()))
			{
				return UrlByteReader.newUrlByteReader(theIDFile, readFile);
			}
			else
			{
				return new FileByteReader(theIDFile, readFile);
			}

		}


		/* Setters for identification status */
		/** Set identification status to Positive */
		public void SetPositiveIdentification() { this.myIDFile.setPositiveIdent(); }

		/** Set identification status to Tentative */
		public void SetTentativeIdentification() { this.myIDFile.setTentativeIdent(); }

		/** Set identification status to No identification */
		public void SetNoIdentification() { this.myIDFile.setNoIdent(); }

		/** Set identification status to Error */
		public void SetErrorIdentification() { this.myIDFile.setErrorIdent(); }

		/** Checks whether the file has yet been classified */
		public bool IsClassified() { return this.myIDFile.isClassified(); }

		/** Get classification of the file */
		public int GetClassification() { return this.myIDFile.getClassification(); }

		/**
		* Set identification warning
		* @param theWarning the warning message to use
		*/
		public void SetIdentificationWarning(string theWarning)
		{
			this.myIDFile.setWarning(theWarning);
		}

		/** Get any warning message created when identifying this file */
		public string GetIdentificationWarning()
		{
			return myIDFile.getWarning();
		}

		/**
		* Add another hit to the list of hits for this file.
		* @param theHit The <code>FileFormatHit</code> to be added
		*/
		public void AddHit(FileFormatHit theHit)
		{
			this.myIDFile.addHit(theHit);
		}

		/**
		* Remove a hit from the list of hits for this file.
		* @param theIndex   Index of the hit to be removed
		*/
		public void RemoveHit(int theIndex)
		{
			this.myIDFile.removeHit(theIndex);
		}

		/** Get number of file format hits */
		public int GetNumberOfHits()
		{
			return myIDFile.getNumHits();
		}

		/**
		* Get a file format hit
		* @param theIndex index of the <code>FileFormatHit</code> to get
		* @return the hit associated with <code>theIndex</code>
		*/
		public FileFormatHit GetHit(int theIndex)
		{
			return myIDFile.getHit(theIndex);
		}


		/** Get file path of the associated file */
		public string GetFilePath()
		{
			return this.myIDFile.getFilePath();
		}

		/** Get file name of the associated file */
		public string GetFileName()
		{
			return this.myIDFile.getFileName();
		}
		
		public virtual void SetFileMarker(long markerPosition)
		{
		}
		
		public virtual long GetFileMarker()
		{
			return 0;
		}
		
		public virtual byte GetByte(long fileIndex)
		{
			return 0x0;
		}
		
		public virtual long GetNumberOfBytes()
		{
			return 0;
		}		
	}
}

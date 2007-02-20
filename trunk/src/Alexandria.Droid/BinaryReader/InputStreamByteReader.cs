using System;
using System.Collections.Generic;
using System.Text;
using AlexandriaOrg.Alexandria.Droid.SignatureFile;

namespace AlexandriaOrg.Alexandria.Droid.BinaryReader
{
	public class InputStreamByteReader : StreamByteReader
	{

		/** Creates a new instance of UrlByteReader */
		private InputStreamByteReader(IdentificationFile theIDFile, bool readFile) : base(theIDFile)
		{
			//super(theIDFile);
			if (readFile)
			{
				this.readInputStream();
			}
		}

		/**
		 * Static constructor for class.  Trys to read stream into a buffer. If it doesn't fit, 
		 * save it to a file, and return a FileByteReader with that file.
		 */
		internal static IByteReader newInputStreamByteReader(IdentificationFile theIDFile, bool readFile) // was ByteReader
		{
			InputStreamByteReader byteReader = new InputStreamByteReader(theIDFile, readFile);
			if (byteReader.tempFile == null)
			{
				return byteReader;
			}
			else
			{
				return new FileByteReader(theIDFile, readFile, byteReader.tempFile.FullName); //getPath());
			}
		}

		/** Read data into buffer or temporary file from the <code>System.in</code> input stream.
		 */
		private void readInputStream()
		{
			try
			{
				//TODO: figure out how to port this
				//readStream(System.in);
			}
			catch (System.IO.IOException)
			{
				this.SetErrorIdentification();
				this.SetIdentificationWarning("Input stream could not be read");
			}
		}

		/**
		 * Checks if the path represents the input stream
		 * @param path the path to check
		 * @return <code>true</code> if <code>path</code> is equal to "-", <code>false</code> otherwise
		 */
		public static bool isInputStream(string path)
		{
			if ("-".Equals(path))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}

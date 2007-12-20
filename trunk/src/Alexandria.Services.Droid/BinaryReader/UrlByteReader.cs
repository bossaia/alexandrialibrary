using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;

namespace Alexandria.Droid.BinaryReader
{
	public class UrlByteReader : StreamByteReader
	{
		/** Creates a new instance of UrlByteReader */
		private UrlByteReader(IdentificationFile theIDFile, bool readFile) : base(theIDFile)
		{
			//super(theIDFile);
			if (readFile)
			{
				this.readUrl();
			}

		}

		/**
		 * Static constructor for class.  Trys to read url into a buffer. If it doesn't fit, 
		 * save it to a file, and return a FileByteReader with that file.
		 */
		public static IByteReader newUrlByteReader(IdentificationFile theIDFile, bool readFile)
		{
			UrlByteReader byteReader = new UrlByteReader(theIDFile, readFile);
			if (byteReader.tempFile == null)
			{
				return byteReader;
			}
			else
			{
				return new FileByteReader(theIDFile, readFile, byteReader.tempFile.FullName); //was getPath()
			}
		}

		/** Read data into buffer or temporary file from the url specified by <code>theIDFile</code>.
		 */
		private void readUrl()
		{
			Uri url;
			try
			{
				url = new Uri(myIDFile.getFilePath());
			}
			catch (Exception) //MalformedURLException ex)
			{
				this.SetErrorIdentification();
				this.SetIdentificationWarning("URL is malformed");
				return;
			}
			try
			{
				//TODO: figure out how to port this
				//readStream(url.openStream());
			}
			catch (System.IO.IOException)
			{
				this.SetErrorIdentification();
				this.SetIdentificationWarning("URL could not be read");
			}
		}

		/**
		 * Get a <code>URL<code> object for this path
		 * @param path the path for which to get the URL
		 * @return the URL represented by <code>path</code> or <code>null</code> if 
		 * it cannot be represented
		 */
		public static Uri getURL(String path)
		{
			Uri url = null;
			try
			{
				url = new Uri(path);
				if (url.AbsolutePath.StartsWith("http", true, System.Globalization.CultureInfo.InvariantCulture))
				//url.getProtocol().equalsIgnoreCase("http"))
				{
					return url;
				}
				else
				{
					return null;
				}

			}
			catch (Exception) //MalformedURLException ex)
			{
				return null;
			}

		}

		/**
		 * Check for a valid URL
		 * @param path the URL to check
		 * @return <code>true</code> if <code>path</code> is a valid URL
		 */
		public static bool isURL(string path)
		{

			Uri url = null;
			try
			{
				url = new Uri(path);
				if (url.AbsolutePath.StartsWith("http", true, System.Globalization.CultureInfo.InvariantCulture))
				//.getProtocol().equalsIgnoreCase("http"))
				{
					return true;
				}
			}
			catch (Exception) //MalformedURLException ex)
			{
				return false;
			}

			return false;

		}
	}
}

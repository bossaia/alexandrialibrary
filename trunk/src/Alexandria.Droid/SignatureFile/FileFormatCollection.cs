using System;
using System.Collections.Generic;
using System.Text;
using AlexandriaOrg.Alexandria.Droid.XmlReader;

namespace AlexandriaOrg.Alexandria.Droid.SignatureFile
{
	public class FileFormatCollection : SimpleElement
	{
		#region Private Fields
		IList<FileFormat> formats = new List<FileFormat>(); //ArrayList();
		#endregion

		#region Public Methods
		/* setters */
		public void addFileFormat(FileFormat format) { formats.Add(format); }
		public void setFileFormats(IList<FileFormat> formats) { this.formats = formats; }

		/* getters */
		public IList<FileFormat> getFileFormats() { return formats; }
		#endregion
	}
}

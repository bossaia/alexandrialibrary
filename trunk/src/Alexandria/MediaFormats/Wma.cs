using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria.MediaFormats
{
	public class Wma : MediaFileFormat
	{
		public Wma() : base("Windows Media Audio", new MimeType(ContentType.Audio, "wma"), true)
		{
			List<FileExtension> extensions = new List<FileExtension>();
			extensions.Add(FileExtension.Wma);
			
			this.AllowedFileExtensions = extensions;
			this.DefaultFileExtension = FileExtension.Wma;
		}
	}
}

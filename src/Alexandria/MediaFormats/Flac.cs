using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria.MediaFormats
{
	public class Flac : MediaFileFormat
	{
		public Flac() : base("Free Lossless Audio Codec", new MimeType(ContentType.Audio, "flac"), true)
		{
			List<FileExtension> extensions = new List<FileExtension>();
			extensions.Add(FileExtension.Flac);
			
			this.AllowedFileExtensions = extensions;
			this.DefaultFileExtension = FileExtension.Flac;
		}
	}
}

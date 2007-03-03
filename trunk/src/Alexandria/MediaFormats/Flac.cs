using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.MediaFormats
{
	public class Flac : MediaFileFormat
	{
		public Flac() : base("Free Lossless Audio Codec", new System.Net.Mime.ContentType("audio/flac"), true)
		{
			List<FileExtension> extensions = new List<FileExtension>();
			extensions.Add(FileExtension.Flac);
			
			this.AllowedFileExtensions = extensions;
			this.DefaultFileExtension = FileExtension.Flac;
		}
	}
}

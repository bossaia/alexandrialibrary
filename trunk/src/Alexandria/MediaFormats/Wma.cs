using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.MediaFormats
{
	public class Wma : MediaFileFormat
	{
		public Wma() : base("Windows Media Audio", new System.Net.Mime.ContentType("audio/wma"), true)
		{
			List<FileExtension> extensions = new List<FileExtension>();
			extensions.Add(FileExtension.Wma);
			
			this.AllowedFileExtensions = extensions;
			this.DefaultFileExtension = FileExtension.Wma;
		}
	}
}

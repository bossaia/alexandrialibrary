using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.MediaFormats
{
	public class Flac : MediaFileFormat
	{
		public Flac() : base("Free Lossless Audio Codec", new System.Net.Mime.ContentType("audio/flac"), true)
		{
			List<string> extensions = new List<string>();
			extensions.Add("flac");
			
			this.AllowedFileExtensions = extensions;
			this.DefaultFileExtension = "flac";
		}
	}
}
